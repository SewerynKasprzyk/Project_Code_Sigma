using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 10, corridorCount = 5;
    [SerializeField]
    [Range(1, 3)]
    private int corridorWidth = 2;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomChance = 0.25f;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstDungeonGeneration();
    }

    private void CorridorFirstDungeonGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnds(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corridors.Count; i++)
        {
            if(corridorWidth == 2)
            {
                corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
            }

            if (corridorWidth == 3)
            {
                corridors[i] = IncreaseCorridorBrush3By3(corridors[i]);
            }

            floorPositions.UnionWith(corridors[i]);

        }

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private List<Vector2Int> IncreaseCorridorBrush3By3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();

        for (int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }

    private List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previewDirection = Vector2Int.zero;

        for (int i = 1; i < corridor.Count; i++)
        {
            Vector2Int direction = corridor[i] - corridor[i - 1];
            if(previewDirection != Vector2Int.zero && direction != previewDirection)
            {
                for(int x = -1; x <= 2; x++)
                {
                    for(int y = -1; y <= 2; y++)
                    {
                        newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                    }
                }
                previewDirection = direction;
            }

            else
            {
                Vector2Int newTileOffset = GetDirection90From(direction);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newTileOffset);
            }
        }

        return newCorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
           return Vector2Int.right;
        }
        else if(direction == Vector2Int.down)
        {
            return Vector2Int.left;
        }
        else if(direction == Vector2Int.left)
        {
            return Vector2Int.up;
        }
        else if(direction == Vector2Int.right)
        {
            return Vector2Int.down;
        }

        return Vector2Int.zero;
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var deadEnd in deadEnds)
        {
            if (roomFloors.Contains(deadEnd) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, deadEnd);
                roomFloors.UnionWith(room);
            }
        }   
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (var position in floorPositions)
        {
            int neighbourCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighbourCount++;
                }
            }

            if (neighbourCount == 1)
            {
                deadEnds.Add(position);
            }
        }

        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomChance);

        List<Vector2Int> potentialRoomPositionsList = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var position in potentialRoomPositionsList)
        {
            var room = RunRandomWalk(randomWalkParameters, position);
            roomPositions.UnionWith(room);
        }
        return roomPositions;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();


        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }

        return corridors;
    }
}
