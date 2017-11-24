using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour {

    private Grid grid;

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

    private void Start()
    {
        grid = new Grid((int)gridSize.x,(int)gridSize.y);
       
        create();
    }

    private void create()
    {
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
        checkObstacles(obj);
        objs.Add(obj);
    }

    private void checkObstacles(GameObject obj)
    {
        if (obj.layer != obstacleMask)
            return;

        int x = (int)obj.transform.position.x;
        int y = (int)obj.transform.position.y;

        grid.GetNode(x, y).IsWalkable = false;
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
}
