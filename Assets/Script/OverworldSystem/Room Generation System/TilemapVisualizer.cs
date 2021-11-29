using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop, wallBottom, wallRight, wallLeft, wallTopRight, wallTopLeft,
                     wallDownRight, wallDownLeft, wallSingle, wallCornerRightTop, wallCornerLeftTop,
                     wallCornerRightDown, wallCornerLeftDown, wallHorizontalSide, wallVerticalSide,
                     wallHorCornerOneUp, wallHorCornerOneDown, wallVerCornerOneLeft, wallVerCornerOneRight;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos)
    {
        PaintTiles(floorPos, floorTilemap, floorTile);
    }

    public void PaintSingleBasicWall(Vector2Int pos, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallTypeData.wallTop.Contains(typeAsInt)) { tile = wallTop; } else 
        if (WallTypeData.wallSideRight.Contains(typeAsInt)) { tile = wallRight; } else 
        if (WallTypeData.wallSideLeft.Contains(typeAsInt)) { tile = wallLeft; } else 
        if (WallTypeData.wallBottom.Contains(typeAsInt)) { tile = wallBottom; }

        if (tile != null)
            PaintSingleTile(wallTilemap, tile, pos);
    }

    ///FIX THIS ONEEE!! OR JUST USE AUTO TILE
    public void PaintSingleCornerWall(Vector2Int pos, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallTypeData.wallCornerFacingUpRight.Contains(typeAsInt)) { tile = wallCornerLeftTop; } else 
        if (WallTypeData.wallCornerFacingUpLeft.Contains(typeAsInt)) { tile = wallCornerRightTop; } else
        if (WallTypeData.wallCornerFacingDownRight.Contains(typeAsInt)) {tile = wallCornerLeftDown; } else 
        if (WallTypeData.wallCornerFacingDownLeft.Contains(typeAsInt)) { tile = wallCornerRightDown; } else 
        if (WallTypeData.wallDiagonalCornerDownLeft.Contains(typeAsInt)) { tile = wallDownLeft; } else 
        if (WallTypeData.wallDiagonalCornerDownRight.Contains(typeAsInt)) { tile = wallDownRight; } else 
        if (WallTypeData.wallDiagonalCornerUpLeft.Contains(typeAsInt)) { tile = wallTopLeft; } else
        if (WallTypeData.wallDiagonalCornerUpRight.Contains(typeAsInt)) { tile = wallTopRight; } else 
        if (WallTypeData.wallHorizontalSideOne.Contains(typeAsInt)) { tile = wallHorizontalSide; } else 
        if (WallTypeData.wallVerticalSideOne.Contains(typeAsInt)) { tile = wallVerticalSide; } else 
        if (WallTypeData.wallVerticalCornerOneLeft.Contains(typeAsInt)) { tile = wallVerCornerOneLeft; } else
        if (WallTypeData.wallVerticalCornerOneRight.Contains(typeAsInt)) { tile = wallVerCornerOneRight; } else 
        if (WallTypeData.wallHorizontalCornerOneDown.Contains(typeAsInt)) { tile = wallHorCornerOneDown; } else 
        if (WallTypeData.wallHorizontalCornerOneUp.Contains(typeAsInt)) { tile = wallHorCornerOneUp; }

        if (tile != null)
            PaintSingleTile(wallTilemap, tile, pos);
    }

    private void PaintTiles(IEnumerable<Vector2Int> pos, Tilemap tilemap, TileBase tile)
    {
        foreach (var _pos in pos)
        {
            PaintSingleTile(tilemap, tile, _pos);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int pos)
    {
        var tilePos = tilemap.WorldToCell((Vector3Int)pos);
        tilemap.SetTile(tilePos, tile);
    }

    public void Clear()
    {
        ItemEnemyGeneration.ClearObject();
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
