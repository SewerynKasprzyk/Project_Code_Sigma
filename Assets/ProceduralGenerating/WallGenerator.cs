using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPosition = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);

        foreach (var position in basicWallPosition)
        {
            tilemapVisualizer.PaintSingleBasicWallTile(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbour = position + direction;
                if (!floorPositions.Contains(neighbour))
                {
                    wallPositions.Add(neighbour);
                }
            }
        }

        return wallPositions;
    }

}
