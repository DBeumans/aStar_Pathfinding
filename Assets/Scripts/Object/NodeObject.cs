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
            if (KeyboardInput.Ctrl && MouseBehaviour.MouseLeft)
            {
                for (int i = 0; i < grid.Objects.Count; i++)
                {
                    grid.Grid.GetNode(grid.Objects[i].transform.position).IsStart = false;
                }
                reset();

                grid.Grid.GetNode(position).IsStart = true;
                return;
            }

            //Check if the node is not a end node.
            if (grid.Grid.IsEndNode(position))
                return;

            grid.Grid.GetNode(position).IsWalkable = false;
        }

        if (MouseBehaviour.MouseRight)
        {
            
            for (int i = 0; i < grid.Objects.Count; i++)
            {
                Vector2 objPos = grid.Objects[i].transform.position;
                grid.Grid.GetNode(objPos).IsEnd = false;
            }
            reset();

            grid.Grid.GetNode(position).IsEnd = true;
        }


        
    }

    private void reset()
    {
        for (int i = 0; i < grid.Objects.Count; i++)
        {
            Vector2 objPos = grid.Objects[i].transform.position;
            if (grid.Grid.GetNode(objPos).IsWalkable == false || grid.Grid.GetNode(objPos).IsStart == false)
                continue;

            grid.Grid.GetNode(objPos).Reset();
            grid.PathNodes = null;
        }
    }

}
