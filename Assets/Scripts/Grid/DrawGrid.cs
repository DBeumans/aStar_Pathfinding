using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGrid : MonoBehaviour {

    private CreateGrid grid;
 
    private void Start()
    {
        grid = GetComponent<CreateGrid>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, grid.GridSize);
    }
}
