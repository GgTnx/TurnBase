using System;
using PlayerScripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public List<Vector2> _pathToTarget;
    private List<Node> CheckedNodes;
    private List<Node> WaitingNodes;
    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _layerMask;

    private void Start()
    {
       
    }

    private void Update()
    {
        
       
    }

   
    public List<Vector2> GetPath(Vector2 start, Vector2 targetPosition) 
    {
        CheckedNodes = new List<Node>();
        WaitingNodes = new List<Node>();
        var pathToTarget = new List<Vector2>();
        Vector2 startPosition = start;
        if (startPosition == targetPosition)
            return pathToTarget;
        Node startNode = new Node(0, startPosition,targetPosition, null);
        CheckedNodes.Add(startNode);
        WaitingNodes.AddRange(GetNeighboursNodes(startNode));
        while (WaitingNodes.Count>0)
        {
            Node nodetocheck = WaitingNodes.FirstOrDefault(x => x.F == WaitingNodes.Min(y => y.F)); 
            if (nodetocheck.Position == targetPosition)
                return CalculatePath(nodetocheck);
            var walkable = !Physics2D.OverlapCircle(new Vector2(nodetocheck.Position.x + 0.5f, nodetocheck.Position.y + 0.5f),0.05f, _layerMask);
            if (!walkable)
            {
                WaitingNodes.Remove(nodetocheck);
                CheckedNodes.Add(nodetocheck);
            }
            else
            {
                WaitingNodes.Remove(nodetocheck);
                if (CheckedNodes.All(x => x.Position != nodetocheck.Position))
                {
                    CheckedNodes.Add(nodetocheck);
                    WaitingNodes.AddRange(GetNeighboursNodes(nodetocheck));
                }
                
            }
        }
        

        return _pathToTarget;
    }

    public List<Vector2> MovePath(Vector2 startPosition, Vector2 endPosition)
    {
        return GetPath(startPosition, endPosition);
    }

    private List<Vector2> CalculatePath(Node node)
    {
        var path = new List<Vector2>();
        Node currentNode = node;
        while (currentNode.PreviosNode !=null)
        {
            path.Add(new Vector2(currentNode.Position.x+0.5f,currentNode.Position.y+0.5f));
            currentNode = currentNode.PreviosNode;
        }
        

        return path;
    }

    private List<Node> GetNeighboursNodes(Node node)
    {
        List<Node> Neighbors = new List<Node>();
        Neighbors.Add(new Node(node.G+1,new Vector2(node.Position.x-1,node.Position.y),node.TargetPosition,node));
        Neighbors.Add(new Node(node.G+1,new Vector2(node.Position.x+1,node.Position.y),node.TargetPosition,node));
        Neighbors.Add(new Node(node.G+1,new Vector2(node.Position.x,node.Position.y-1),node.TargetPosition,node));
        Neighbors.Add(new Node(node.G+1,new Vector2(node.Position.x,node.Position.y+1),node.TargetPosition,node));

        return Neighbors;
    }
}
