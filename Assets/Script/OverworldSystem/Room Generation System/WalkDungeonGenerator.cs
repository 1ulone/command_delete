using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected RoomGenerationData roomData;

    protected override void RunProceduralGeneration()
    { 
        HashSet<Vector2Int> floorPos = RunRandomWalk(roomData, startPosition);
        tmapVisualizer.Clear();
        tmapVisualizer.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tmapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(RoomGenerationData parameters, Vector2Int pos)
    {
        var curPos = pos;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProcuderalGenerationAlgorithm.simpleWalk(curPos, parameters.walkLen);
            floorPos.UnionWith(path);
            if (parameters.startRandomIteration)
                curPos = floorPos.ElementAt(Random.Range(0, floorPos.Count));
        }

        return floorPos;
    }
}