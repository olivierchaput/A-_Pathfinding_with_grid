using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCustom : MonoBehaviour
{
    public Transform StartPosition;
    public LayerMask obstacleMask;

    public Vector2 gridWorldSize;

    public float nodeRadius;
    public float distanceFromNextNode;

    Node[,] grid;
    public List<Node> FinalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool obstacle = false;

                if(Physics.CheckSphere(worldPoint, nodeRadius, obstacleMask))
                {
                    obstacle = true;
                }

                grid[x, y] = new Node(obstacle, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 _WorldPosition)
    {
        float xPoint = ((_WorldPosition.x + gridWorldSize.x/2) / gridWorldSize.x);
        float yPoint = ((_WorldPosition.z + gridWorldSize.y/2) / gridWorldSize.y);

        xPoint = Mathf.Clamp01(xPoint);
        yPoint = Mathf.Clamp01(yPoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xPoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPoint);

        return grid[x, y];

    }

    public List<Node> GetNeighbouringNodes(Node _Node)
    {

        List<Node> NeighbouringNodes = new List<Node>();

        for(int x = -1; x <= 1; x++) 
        {
            for (int y = -1; y <= 1; y++) 
            {
                //if we are on the node tha was passed in, skip this iteration.
                if(x == 0 && y == 0) 
                {
                    continue;
                }

                int checkX = _Node.gridX + x;
                int checkY = _Node.gridY + y;

                //Make sure the node is within the grid.
                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) 
                {
                    NeighbouringNodes.Add(grid[checkX, checkY]); //Adds to the neighbours list.
                }
            }
        }

        return NeighbouringNodes;
    } 

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null)
        {
            foreach (Node node in grid)
            {
                if (!node.isObstacle)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }
                if (FinalPath != null)
                {
                    if(FinalPath.Contains(node))
                    {
                        Gizmos.color = Color.red;
                    }   
                }
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - distanceFromNextNode));
            }
        }
    }

}
