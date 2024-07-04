using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull, wallInnerCornerDownLeft, wallInnerCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerDownRight, wallDiagonalCornerUpLeft, wallDiagonalCornerUpRight;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    public void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }

    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
       var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleBasicWallTile(Vector2Int position, string binaryType)
    {
        int typAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if(WallTypesHelper.wallTop.Contains(typAsInt))
        {
            tile = wallTop;
        }
        else if(WallTypesHelper.wallSideLeft.Contains(typAsInt))
        {
            tile = wallSideLeft;
        }
        else if(WallTypesHelper.wallSideRight.Contains(typAsInt))
        {
            tile = wallSideRight;
        }
        else if(WallTypesHelper.wallBottom.Contains(typAsInt))
        {
            tile = wallBottom;
        }
        else
        {
            tile = wallFull;
        }


        if (tile!=null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }
    }

    internal void PaintSingleCornerWallTile(Vector2Int position, string binaryType)
    {
        int typAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if(WallTypesHelper.wallInnerCornerDownLeft.Contains(typAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typAsInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallBottomEightDirections.Contains(typAsInt))
        {
            tile = wallBottom;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typAsInt))
        {
            tile = wallTop;
        }

        if (tile != null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }
    }
}
