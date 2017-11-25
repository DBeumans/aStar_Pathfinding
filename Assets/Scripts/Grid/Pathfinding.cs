using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    CreateGrid grid;

    [SerializeField]
    Vector2 startPosition;

    Vector2 endPosition;

    private void Start()
    {
        grid = GetComponent<CreateGrid>();
    }

    private void Update()
    {
        getEndPosition();

        FindPath(startPosition, endPosition);
    }

    private void getEndPosition()
    {
        //loop through the objs and check if there is a object with isEnd bool set to true.
        for (int i = 0; i < grid.Objects.Count; i++)
        {
            Vector2 objPos = grid.Objects[i].transform.position;
            Node node = grid.Grid.GetNode(objPos);
            if (!node.IsEnd)
                continue;


            endPosition = node.WorldPosition;
        }
    }

    private void FindPath(Vector2 startPos, Vector2 endPos)
    {
        Node startNode = grid.Grid.GetNode(startPos);
        Node endNode = grid.Grid.GetNode(endPos);

        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();
        openSet.Add(startNode);
        
        while (openSet.Count > 0)
        {
            
            Node currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost)
                    if(openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
                        currentNode = openSet[i];
            }
            

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            
            if (currentNode == endNode)
            {
                retracePath(startNode, endNode);
                return;
            }
            

            foreach (Node neighbour in grid.Grid.GetNeighbours(currentNode.WorldPosition))
            {
                if (!neighbour.IsWalkable || closedSet.Contains(neighbour))
                    continue;
                

                int newMovementCost = currentNode.GCost + getDistance(currentNode, neighbour);
                if(newMovementCost < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = newMovementCost;
                    neighbour.FCost = getDistance(neighbour, endNode);
                    neighbour.Parent = currentNode;
                    
                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);

                }
            }
        }
    }

    private void retracePath(Node startNode, Node endNode)
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
        int distanceX = Mathf.Abs((int)nodeA.WorldPosition.x - (int)nodeB.WorldPosition.x);
        int distanceY = Mathf.Abs((int)nodeA.WorldPosition.y - (int)nodeB.WorldPosition.y);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
