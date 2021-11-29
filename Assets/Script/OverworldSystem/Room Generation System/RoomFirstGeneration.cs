using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstGeneration : WalkDungeonGenerator
{
    public static Vector3 playernextpos;

    [SerializeField]
    private Transform PlayerPosition, movePointPosition;
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRoom = false;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomList = ProcuderalGenerationAlgorithm.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, 
                       new Vector3Int(dungeonWidth, dungeonHeight, 0)),
                       minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRoom) { floor = CreateRandomRooms(roomList); } else 
        if (!randomWalkRoom) { floor = CreateSimpleRooms(roomList); }

        List<Vector2Int> roomCenter = new List<Vector2Int>();
        List<Vector2Int> interestsPointPosition = new List<Vector2Int>();
        foreach (var room in roomList)
        {
            roomCenter.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
            PlayerPosition.position = new Vector3(roomCenter[0].x, roomCenter[0].y, 0);
            movePointPosition.position = PlayerPosition.position;
            interestsPointPosition = PointOfInterest(roomCenter);
        }

        HashSet<Vector2Int> corridor = ConnectRooms(roomCenter);
        floor.UnionWith(corridor);

        tmapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tmapVisualizer);
        ItemEnemyGeneration.CreateObject(interestsPointPosition, roomData);
    }

    private List<Vector2Int> PointOfInterest(List<Vector2Int> roomCount)
    {
        List<Vector2Int> Interestpoint = new List<Vector2Int>();
        Vector2Int ppos = new Vector2Int((int)PlayerPosition.position.x, (int)PlayerPosition.position.y);
        foreach(var roompos in roomCount)
        {
            if (Vector2.Distance(roompos, ppos) >= minRoomWidth || Vector2.Distance(roompos, ppos) >= minRoomHeight)
            {
                Interestpoint.Add(roompos);
            }
        }
        
        return Interestpoint;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> rCenter)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var currRoomCenter = rCenter[Random.Range(0, rCenter.Count)];
        rCenter.Remove(currRoomCenter);

        while (rCenter.Count > 0)
        {
            Vector2Int closest = FindClosestPoint(currRoomCenter, rCenter);
            rCenter.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currRoomCenter, closest);
            currRoomCenter = closest;
            corridor.UnionWith(newCorridor);
        }
        return corridor;
    }

    private Vector2Int FindClosestPoint(Vector2Int curCenter, List<Vector2Int> RoomCenter)
    {
        Vector2Int closest = Vector2Int.zero;
        float dist = float.MaxValue;

        foreach (var pos in RoomCenter)
        {
            float curDist = Vector2.Distance(pos, curCenter);
            if (curDist < dist)
            {
                dist = curDist;
                closest = pos;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int curCenter, Vector2Int closestPoint)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var pos = curCenter;
        corridor.Add(pos);
        while(pos.y != closestPoint.y)
        {
            if (closestPoint.y > pos.y) { pos += Vector2Int.up; } else
            if (closestPoint.y < pos.y) { pos += Vector2Int.down; }
            corridor.Add(pos);
        }
        while(pos.x != closestPoint.x)
        {
            if (closestPoint.x > pos.x) { pos += Vector2Int.right; } else
            if (closestPoint.x < pos.x) { pos += Vector2Int.left; }
            corridor.Add(pos);
        }
        return corridor;
    }

    private HashSet<Vector2Int> CreateRandomRooms(List<BoundsInt> list)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < list.Count; i++)
        {
            var roomBounds = list[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(roomData, roomCenter);
            foreach (var pos in roomFloor)
            {
                if (pos.x >= (roomBounds.xMin + offset) && pos.x <= (roomBounds.xMax - offset) && 
                    pos.y >= (roomBounds.yMin - offset) && pos.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(pos);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> list)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in list)
        {
            for (int i = offset; i < room.size.x - offset; i++)
            {
                for (int r = offset; r < room.size.y - offset; r++)
                {
                    Vector2Int pos = (Vector2Int)room.min + new Vector2Int(i, r);
                    floor.Add(pos);
                }
            }
        }
        return floor;
    }
}