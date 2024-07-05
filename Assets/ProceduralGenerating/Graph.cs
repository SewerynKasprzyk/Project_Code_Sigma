using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    private static List<Vector2Int> neighbours4directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // UP
        new Vector2Int(1, 0), // RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0) // LEFT
    };

    private static List<Vector2Int> neighbours8directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // UP
        new Vector2Int(1, 0), // RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0), // LEFT
        new Vector2Int(1, 1), // Diagonal UP-RIGHT
        new Vector2Int(1, -1), // Diagonal DOWN-RIGHT
        new Vector2Int(-1, -1), // Diagonal DOWN-LEFT
        new Vector2Int(-1, -1) // Diagonal UP-LEFT
    };

    List<Vector2Int> graph;

    public Graph(IEnumerable<Vector2Int> vertices)
    {
        this.graph = new List<Vector2Int>(vertices);
    }

    public List<Vector2Int> GetNeighbours4Directions(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, neighbours4directions);
    }
    
    public List<Vector2Int> GetNeighbours8Directions(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, neighbours8directions);
    }

    private List<Vector2Int> GetNeighbours(Vector2Int startPosition, List<Vector2Int> neighboursOffsetList)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        foreach (var neighbourDirection in neighboursOffsetList)
        {
            Vector2Int neighbour = startPosition + neighbourDirection;
            if (graph.Contains(neighbour))
            {
                neighbours.Add(neighbour);
            }
        }
        return neighbours;
    }
}
