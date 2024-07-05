using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;

    [SerializeField]
    [Range(1, 3)]
    private int corridorWidth = 2;

    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;

    [SerializeField]
    private bool randomWalkRooms = false;

    private DungeonData dungeonData;

    public UnityEvent OnFinishedRoomGeneration;


    override protected void RunProceduralGeneration()
    {
        dungeonData = FindAnyObjectByType<DungeonData>();
        if (dungeonData == null)
        {
            dungeonData = new GameObject("DungeonData").AddComponent<DungeonData>();
        }


        CreateRooms();

        OnFinishedRoomGeneration.Invoke();
    }

    private void CreateRooms()
    {

        dungeonData.Reset();

        var roomList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floorPositions = CreateRoomsRandomly(roomList);
        }
        else
        {
            floorPositions = CreateSimpleRooms(roomList);
        }

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        dungeonData.Path = corridors;

        floorPositions.UnionWith(corridors);


        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < roomList.Count; i++)
        {
            var room = roomList[i];
            Vector2Int roomCenter = new Vector2Int(Mathf.RoundToInt(room.center.x), Mathf.RoundToInt(room.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);

            foreach (var position in roomFloor)
            {
                if (position.x >= (room.xMin + offset) && position.x <= (room.xMax - offset) && position.y >= (room.yMin - offset) && position.y <= (room.yMax - offset))
                {
                    floorPositions.Add(position);
                }
            }

            Room room1 = new Room(roomCenter, floorPositions);
            dungeonData.Rooms.Add(room1);
        }

        Debug.Log("Rooms created: " + dungeonData.Rooms.Count);
        return floorPositions;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoom = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoom);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoom, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoom, closest);
            currentRoom = closest;
            corridors.UnionWith(newCorridor);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoom, Vector2Int closest)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoom;
        corridor.Add(position);

        while (position.y != closest.y)
        {
            if (position.y < closest.y)
            {
                position += Vector2Int.up;
            }
            else if (position.y > closest.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }

        while (position.x != closest.x)
        {
            if (position.x < closest.x)
            {
                position += Vector2Int.right;
            }
            else if (position.x > closest.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }

        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoom, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;

        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2Int.Distance(position, currentRoom);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }

        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        foreach (var room in roomList)
        {
            for (int column = offset; column < room.size.x - offset; column++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(column, row);
                    floorPositions.Add(position);
                }
            }

            Vector2Int roomCenter = new Vector2Int(Mathf.RoundToInt(room.center.x), Mathf.RoundToInt(room.center.y));

            Room room1 = new Room(roomCenter, floorPositions);
            dungeonData.Rooms.Add(room1);
        }

        Debug.Log("Rooms created: " + dungeonData.Rooms.Count);
        return floorPositions;
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
            if (previewDirection != Vector2Int.zero && direction != previewDirection)
            {
                for (int x = -1; x <= 2; x++)
                {
                    for (int y = -1; y <= 2; y++)
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
        else if (direction == Vector2Int.down)
        {
            return Vector2Int.left;
        }
        else if (direction == Vector2Int.left)
        {
            return Vector2Int.up;
        }
        else if (direction == Vector2Int.right)
        {
            return Vector2Int.down;
        }

        return Vector2Int.zero;
    }
}
