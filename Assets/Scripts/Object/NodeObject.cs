using UnityEngine;
using System.Collections;

public class NodeObject : MonoBehaviour {

    private Renderer render;

    private CreateGrid grid;

    private Vector2 position;

    private void Start()
    {
        render = GetComponent<Renderer>();
        grid = FindObjectOfType<CreateGrid>();

        position = new Vector2(this.transform.position.x, this.transform.position.y);
    }

    private void OnMouseOver()
    {
        if (MouseBehaviour.MouseLeft)
        {
            //Check if the node is not a end node.
            if (grid.Grid.IsEndNode(position))
                return;

            grid.Grid.GetNode(position).IsWalkable = false;
        }
        if(MouseBehaviour.MouseRight)
        {
            for (int i = 0; i < grid.Objects.Count; i++)
            {
                grid.Grid.GetNode(grid.Objects[i].transform.position).IsEnd = false;
            }

            grid.Grid.GetNode(position).IsEnd = true;
        }
        
    }

}
