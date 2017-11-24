using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    private Node[,] grid;
    public Node[,] GetGrid { get { return grid; } }

    private int gridWidth;
    public int GridWidth { get { return gridWidth; } }

    private int gridHeight;
    public int GridHeight { get { return gridHeight; } }

    public Grid(int width, int height)
    {
        // checks if the width and / or height are higher than 0.
        if (width <= 0 || height <= 0)
            return;

        gridWidth = width;
        gridHeight = height;

        grid = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var currentPos = new Vector2(x, y);
                grid[x, y] = new Node(currentPos);
            }
        }
    }

    public List<Node>GetNeighbours(Vector2 pos)
    {
        List<Node> neighbours = new List<Node>();

        int x = (int)pos.x;
        int y = (int)pos.y;

        for (int i = -1; i < 1; i++)
        {
            for (int j = -1; j < 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                if(IsOnGrid(x + i, y + j))
                {
                    neighbours.Add(GetNode(x + i, y + j));
                }
            }
        }

        return neighbours;
    }

    /// <summary>
    /// Gets a node from the grid.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Node GetNode(Vector2 pos)
    {
        return GetNode((int)pos.x, (int)pos.y);
    }

    /// <summary>
    /// Gets a node from the grid.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Node GetNode(int x , int y)
    {
        if (!IsOnGrid(x, y))
            return null;
        return grid[x, y];
    }

    /// <summary>
    /// Checks if the X and Y are within the grid.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsOnGrid(int x, int y)
    {
        return x >= 0 && y >= 0 && x < gridWidth && y < gridHeight;
    }

    /// <summary>
    /// Checks if the position is within the grid.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool IsOnGrid(Vector2 pos)
    {
        return IsOnGrid((int)pos.x, (int)pos.y);
    }
}
