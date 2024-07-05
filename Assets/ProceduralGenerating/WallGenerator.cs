using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPosition = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallPosition = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);

        CreateBasicWall(tilemapVisualizer, basicWallPosition, floorPositions);
        CreateCornerWalls(tilemapVisualizer, cornerWallPosition, floorPositions);
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPosition, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPosition)
        {
            string neigboursBinary = "";

            foreach (var direction in Direction2D.eightDirectionList)
            {
                var neighbour = position + direction;
                if (floorPositions.Contains(neighbour))
                {
                    neigboursBinary += "1";
                }
                else
                {
                    neigboursBinary += "0";
                }
            }

            tilemapVisualizer.PaintSingleCornerWallTile(position, neigboursBinary);
        }
    }

    private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPosition, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPosition)
        {
            string neigboursBinary = "";

            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                var neighbour = position + direction;
                if (floorPositions.Contains(neighbour))
                {
                    neigboursBinary += "1";
                }
                else
                {
                    neigboursBinary += "0";
                }
            }

            tilemapVisualizer.PaintSingleBasicWallTile(position, neigboursBinary);
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
