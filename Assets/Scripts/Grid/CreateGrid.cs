using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour {

    private Grid grid;
    public Grid Grid { get { return grid; } }

    [SerializeField]
    private LayerMask obstacleMask;

    [SerializeField]
    private GameObject gridObjectPrefab;

    private Vector3 worldBottomLeft;
    private Vector3 worldPoint;
    public Vector3 WorldPoint
    {
        get { return worldPoint; }
    }

    [SerializeField]
    private Vector2 gridSize;

    public Vector2 GridSize
    {
        get { return gridSize; }
    }

    [SerializeField]
    private List<GameObject> objs = new List<GameObject>();
    public List<GameObject> Objects
    {
        get { return objs; }
    }

    private List<Node> pathNodes = new List<Node>();
    public List<Node> PathNodes
    {
        get { return pathNodes; }
        set { pathNodes = value; }
    }

    private void Start()
    {
        create();
    }

    private void create()
    {
        grid = new Grid((int)gridSize.x, (int)gridSize.y);

        worldBottomLeft = transform.position - Vector3.right * grid.GridWidth / 2 - Vector3.up * grid.GridHeight / 2;
        worldPoint = transform.position + Vector3.right * grid.GridWidth / 2 + Vector3.up * grid.GridHeight / 2;        

        for (int x = 0; x < grid.GridWidth; x++)
        {
            for (int y = 0; y < grid.GridHeight; y++)
            { 
                instantiateObject(x , y);
            }
        }
    }
    
    private void instantiateObject(int x, int y)
    {
        GameObject obj = GameObject.Instantiate(gridObjectPrefab);
        obj.transform.position = new Vector2(x, y);
        obj.name = obj.name + " " + x + " " + y;
        setWalls();
        objs.Add(obj);
    }
    
    private void setWalls()
    {
        grid.GetNode(0, 0).IsWalkable = false;

        //setColors();
    }

    private void setColors()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            Renderer render = objs[i].GetComponent<Renderer>();

            if (!grid.GetNode((int)objs[i].transform.position.x, (int)objs[i].transform.position.y).IsWalkable)
            {
                render.material.color = Color.red;
            }

            // check if pathNode list position is the same as in the objs list gameobject position.
            for (int j = 0; j < pathNodes.Count; j++)
            {
                Vector2 objPos = objs[i].transform.position;
                if (pathNodes[j].WorldPosition != objPos)
                    continue;
                
                render.material.color = Color.black;
            }
            
        }
        
    }

    private void Update()
    {
        setColors();

        Debug.Log("PathNodes: " +pathNodes.Count);

        // quick reset function
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < objs.Count; i++)
            {
                Node node = grid.GetNode(objs[i].transform.position);
                node.Reset();
            }
        }
    }
}
