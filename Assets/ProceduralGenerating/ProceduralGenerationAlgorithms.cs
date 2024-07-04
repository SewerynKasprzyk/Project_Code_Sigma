using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms
{

    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> rooms = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);

        while (roomsQueue.Count > 0)
        {
            var currentSpace = roomsQueue.Dequeue();
            if (currentSpace.size.x >= minWidth && currentSpace.size.y >= minHeight)
            {
                if (Random.value < 0.5)
                {
                    if (currentSpace.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, currentSpace);
                    }
                    else if (currentSpace.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, currentSpace);
                    }
                    else if (currentSpace.size.x >= minWidth && currentSpace.size.y >= minHeight)
                    {
                        rooms.Add(currentSpace);
                    }
                }
                else
                {

                    if (currentSpace.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, currentSpace);
                    }
                    else if (currentSpace.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, currentSpace);
                    }
                    else if (currentSpace.size.x >= minWidth && currentSpace.size.y >= minHeight)
                    {
                        rooms.Add(currentSpace);
                    }
                }
            }
        }

        return rooms;
    }


    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt currentSpace)
    {
        var xSplit = Random.Range(1, currentSpace.size.x);
        BoundsInt room1 = new BoundsInt(currentSpace.min, new Vector3Int(xSplit, currentSpace.size.y, currentSpace.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(currentSpace.min.x + xSplit, currentSpace.min.y, currentSpace.min.z), new Vector3Int(currentSpace.size.x - xSplit, currentSpace.size.y, currentSpace.size.z));
        roomsQueue.Enqueue(room1); 
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt currentSpace)
    {
        var ySplit = Random.Range(1, currentSpace.size.y);
        BoundsInt room1 = new BoundsInt(currentSpace.min, new Vector3Int(currentSpace.size.x, ySplit, currentSpace.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(currentSpace.min.x, currentSpace.min.y + ySplit, currentSpace.min.z), new Vector3Int(currentSpace.size.x, currentSpace.size.y - ySplit, currentSpace.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), //UP
        new Vector2Int(1, 0), //RIGHT
        new Vector2Int(0, -1), //DOWN
        new Vector2Int(-1, 0) //LEFT
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}
