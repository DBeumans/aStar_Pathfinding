using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    CreateGrid grid;

    [SerializeField]
    Vector2 startPosition;

    [SerializeField]
    Vector2 endPosition;

    private void Start()
    {
        grid = GetComponent<CreateGrid>();
    }

    private void Update()
    {
        FindPath(startPosition, endPosition);
    }

    private void FindPath(Vector2 startPos, Vector2 endPos)
    {
        Node startNode = grid.Grid.GetNode(startPos);
        Node endNode = grid.Grid.GetNode(endPos);

        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();
        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node CurrentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < CurrentNode.FCost)
                    if(openSet[i].FCost == CurrentNode.FCost && openSet[i].HCost < CurrentNode.HCost)
                        CurrentNode = openSet[i];
            }

            openSet.Remove(CurrentNode);
            closedSet.Add(CurrentNode);

            if(CurrentNode == endNode)
            {
                reTracePath(startNode, endNode);
                return;
            }

            foreach(Node neighbour in grid.Grid.GetNeighbours(CurrentNode.Position))
            {
                if (!neighbour.IsWalkable || closedSet.Contains(CurrentNode))
                    continue;

                int newMovementCost = CurrentNode.GCost + getDistance(CurrentNode, neighbour);
                if(newMovementCost < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = newMovementCost;
                    neighbour.FCost = getDistance(neighbour, endNode);
                    neighbour.Parent = CurrentNode;

                    if(!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    private void reTracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while ( currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();

        grid.PathNodes = path;
    }

    private int getDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs((int)nodeA.Position.x - (int)nodeB.Position.x);
        int distanceY = Mathf.Abs((int)nodeA.Position.y - (int)nodeB.Position.y);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
