using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour {

    private Grid grid;
    public Grid Grid { get { return grid; } }

    [SerializeField]
    private GameObject gridObjectPrefab;

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
        objs.Add(obj);
    }

    private void setColors()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            
            Renderer render = objs[i].GetComponent<Renderer>();
            render.material.color = Color.white;
            Vector2 objpos = objs[i].transform.position; 

            if (grid.GetNode(objpos).IsEnd)
            {
                render.material.color = Color.green;
            }

            if (!grid.GetNode(objpos).IsWalkable)
            {
                render.material.color = Color.red;
            }

            if(grid.GetNode(objpos).IsStart)
            {
                render.material.color = Color.yellow;
            }

            // check if pathNode list position is the same as in the objs list gameobject position.
            for (int j = 0; j < pathNodes.Count-1; j++)
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
       
    }
}
