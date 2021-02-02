using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GridCustom grid;
    public Transform StartPosition = null;
    public Transform EndPosition = null;

    LineRenderer lineRenderer;

    /*
    private void Awake()
    {
        grid = GetComponent<GridCustom>();
    }
    */

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void GeneratePath()
    {
        grid.CreateGrid();
        FindPath(StartPosition.position, EndPosition.position);
    }

    void FindPath(Vector3 _startPos, Vector3 _endPos)
    {
        Node startNode = grid.NodeFromWorldPosition(_startPos);
        Node endNode = grid.NodeFromWorldPosition(_endPos);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        bool finalPathFound = false;

        OpenList.Add(startNode);
        
        while(OpenList.Count > 0)
        {

            Node currentNode = OpenList[0];
            
            for (int i = 1; i < OpenList.Count; i++)
            {
                if(OpenList[i].fCost < currentNode.fCost || OpenList[i].fCost == currentNode.fCost && OpenList[i].hCost < currentNode.hCost)
                {
                    currentNode = OpenList[i];
                }
            }

                OpenList.Remove(currentNode);
                ClosedList.Add(currentNode);

                //Debug.Log("Close list count : " + ClosedList.Count);
                //Debug.Log("Open list count : " + OpenList.Count);

                if(currentNode == endNode)
                {
                    GetFinalPath(startNode, endNode);
                    finalPathFound = true;
                }

                foreach(Node neighbourNode in grid.GetNeighbouringNodes(currentNode))
                {
                    if (neighbourNode.isObstacle || ClosedList.Contains(neighbourNode))
                    {
                        continue;
                    }
                    int moveCost = currentNode.gCost + GetManhattanDistance(currentNode, neighbourNode);

                    if (moveCost < neighbourNode.gCost || !OpenList.Contains(neighbourNode))
                    {
                        neighbourNode.gCost = moveCost;
                        neighbourNode.hCost = GetManhattanDistance(neighbourNode, endNode);
                        neighbourNode.Parent = currentNode;

                        if(!OpenList.Contains(neighbourNode))
                        {
                            OpenList.Add(neighbourNode);
                        }
                    }
                }
        }
        if(!finalPathFound)
        {
            Debug.Log("BLOCKED");
        }
    }

    void GetFinalPath(Node _StartNode, Node _EndNode)
    {
        List<Vector3> FinalPathPointsToDraw = new List<Vector3>();
        List<Node> FinalPath = new List<Node>();
        Node currentNode = _EndNode;

        while(currentNode != _StartNode)
        {
            FinalPath.Add(currentNode);
            FinalPathPointsToDraw.Add(currentNode.worldPosition + Vector3.up * 0.02f);
            currentNode = currentNode.Parent;
        }
        FinalPath.Reverse();

        grid.FinalPath = FinalPath;
        lineRenderer.positionCount = FinalPathPointsToDraw.Count;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, FinalPathPointsToDraw[i]);
        }
    }

    private int GetManhattanDistance(Node _currentNode, Node _neighbourNode)
    {
        int x = Mathf.Abs(_currentNode.gridX - _neighbourNode.gridX);
        int y = Mathf.Abs(_currentNode.gridY - _neighbourNode.gridY);

        return x + y;
    }

}
