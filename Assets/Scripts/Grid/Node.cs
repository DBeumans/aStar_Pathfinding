using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    private bool isWalkable;
    public bool IsWalkable { get { return isWalkable; }set { isWalkable = value; } }
    
    private Vector2 worldPosition;
    public Vector2 WorldPosition { get { return worldPosition; } set { worldPosition = value; } }

    public Node(Vector2 worldPos)
    {
        isWalkable = true;
        worldPosition = worldPos;
    }

    private int gCost;
    public int GCost { get { return gCost; } }

    private int hCost;
    public int HCost { get { return hCost; } }

    public int FCost { get { return gCost + hCost; } }

    private Node parent;

    public Node Parent { get { return parent; } }

    private Vector2 position;
    public Vector2 Position { get { return position; } }

}
