using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX, gridY;

    public bool isObstacle;

    public Vector3 worldPosition;

    public Node Parent;

    public int gCost, hCost;
    public int fCost {get {return gCost + hCost;} }

    public Node(bool _isObstacle, Vector3 _pos, int _gridX, int _gridY)
    {
        isObstacle = _isObstacle;
        worldPosition = _pos;
        gridX = _gridX;
        gridY = _gridY;
    }
}
