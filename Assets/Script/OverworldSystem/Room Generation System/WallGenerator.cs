using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TilemapVisualizer tmapVisualizer)
    {
        var basicWallPos = FindWallDirections(floorPos, Direction2D.cardinalDirectionList);
        var cornerWallPos = FindWallDirections(floorPos, Direction2D.diagonalDirectionList);
        CreateBasicWalls(tmapVisualizer, basicWallPos, floorPos);
        CreateCornerWalls(tmapVisualizer, cornerWallPos, floorPos);
    }

    private static void CreateBasicWalls(TilemapVisualizer tilemapvis, HashSet<Vector2Int> pos, HashSet<Vector2Int> floorpos)
    {
        foreach (var _pos in pos)
        {
            string neighboursBinType = "";
            foreach (var dir in Direction2D.cardinalDirectionList)
            {
                var neighbourPos = _pos + dir;
                if (floorpos.Contains(neighbourPos))
                { neighboursBinType += "1"; } else 
                { neighboursBinType += "0"; }
            }
            tilemapvis.PaintSingleBasicWall(_pos, neighboursBinType);
        }
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapvis, HashSet<Vector2Int> pos, HashSet<Vector2Int> floorpos)
    {
        foreach (var _pos in pos)
        {
            string neighboursBinType = "";
            foreach (var dir in Direction2D.eightDirectionList)
            {
                var neighbourPos = _pos + dir;
                if (floorpos.Contains(neighbourPos)) 
                { neighboursBinType += "1"; } else 
                { neighboursBinType += "0"; }
            }
            tilemapvis.PaintSingleCornerWall(_pos, neighboursBinType);
        }
    }

    private static HashSet<Vector2Int> FindWallDirections(HashSet<Vector2Int> floorPos, List<Vector2Int> dirList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach (var pos in floorPos)
        {
            foreach (var dir in dirList)
            {
                var neighbourPos = pos + dir;
                if (floorPos.Contains(neighbourPos) == false)
                    wallPos.Add(neighbourPos);
            }
        }
        return wallPos;
    }
}
