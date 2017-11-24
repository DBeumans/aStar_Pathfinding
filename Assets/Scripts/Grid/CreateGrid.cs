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

    [SerializeField]
    private Vector2 gridSize;

    public Vector2 GridSize { get { return gridSize; } }

    [SerializeField]
    private List<GameObject> objs = new List<GameObject>();

    private List<GameObject> pathNodes = new List<GameObject>();
    public List<GameObject> PathNodes
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
        grid = new Grid( (int) gridSize.x, (int) gridSize.y);

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

        setColors();
    }

    private void setColors()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            if (!grid.GetNode((int)objs[i].transform.position.x, (int)objs[i].transform.position.y).IsWalkable)
            {
                Renderer rend = objs[i].GetComponent<Renderer>();
                rend.material.color = Color.red;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < pathNodes.Count; i++)
        {
            Renderer rend = pathNodes[i].GetComponent<Renderer>();

            rend.material.color = Color.black;
        }
    }

}
