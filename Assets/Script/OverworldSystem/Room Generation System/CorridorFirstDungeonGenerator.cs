using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CorridorFirstDungeonGenerator : WalkDungeonGenerator
{
    [SerializeField]
    private int corridorLen = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();

        CreateCorridors(floorPos, potentialRoomPos);

        HashSet<Vector2Int> roomPos = CreateRoom(potentialRoomPos);
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPos);

        CreateRoomsAtDE(deadEnds, roomPos);

        floorPos.UnionWith(roomPos);
        tmapVisualizer.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tmapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoom(HashSet<Vector2Int> pos)
    {
        HashSet<Vector2Int> roomPos = new HashSet<Vector2Int>();
        int roomCreateCount = Mathf.RoundToInt(pos.Count * roomPercent);
        List<Vector2Int> roomToCreate = pos.OrderBy(x => Guid.NewGuid()).Take(roomCreateCount).ToList();

        foreach (var _roomPos in roomToCreate)
        {
            var roomFloor = RunRandomWalk(roomData, _roomPos);
            roomPos.UnionWith(roomFloor);
        }
        return roomPos;
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> pos)
    {
        List<Vector2Int> deadends = new List<Vector2Int>();
        foreach (var _pos in pos)
        {
            int neighboursCount = 0;
            foreach (var dir in Direction2D.cardinalDirectionList)
            {
                if (pos.Contains(_pos + dir))
                    neighboursCount++;
            }
            if (neighboursCount == 1)
                deadends.Add(_pos);
        }
        return deadends;
    }

    private void CreateRoomsAtDE(List<Vector2Int> dePos, HashSet<Vector2Int> pos)
    {
        foreach (var _pos in dePos)
        {
            if (pos.Contains(_pos) == false)
            {
                var roomFloor = RunRandomWalk(roomData, _pos);
                pos.UnionWith(roomFloor);
            }
        }
    }

    private void CreateCorridors(HashSet<Vector2Int> pos, HashSet<Vector2Int> potentialPos)
    {
        var CurrentPos = startPosition;
        potentialPos.Add(CurrentPos);

        for (int i = 0; i < corridorCount; i++)
        {
            var cpath = ProcuderalGenerationAlgorithm.randomWalkCorridor(CurrentPos, corridorLen);
            CurrentPos = cpath[cpath.Count - 1];
            potentialPos.Add(CurrentPos);
            pos.UnionWith(cpath);
        }
    }
}