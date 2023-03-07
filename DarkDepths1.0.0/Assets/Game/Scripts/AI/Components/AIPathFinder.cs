using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AIPathFinder : MonoBehaviour
{
    [SerializeField]
    private int radiusPath = 5;
    [SerializeField]
    private List<Vector2> freeNodes = new List<Vector2>();

    private List<Node> checkedNodes;
    private List<Node> waitingNodes;

    [SerializeField]
    private LayerMask SolidLayer;

    public List<Vector2> FreeNodes { get => freeNodes; set => freeNodes = value; }

    public List<Vector2> GetFreeNodesRadius()
    {
        checkedNodes = new List<Node>();
        waitingNodes = new List<Node>();
        freeNodes = new List<Vector2>();

        Vector2 StartPosition = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

        Node startNode = new Node(0, StartPosition, null);
        checkedNodes.Add(startNode);
        waitingNodes.AddRange(GetNeighbourNodes(startNode));

        while (waitingNodes.Count > 0) 
        {
            Node nodeToCheck = waitingNodes.Where(x => x.F == waitingNodes.Min(y => y.F)).FirstOrDefault();

            if (nodeToCheck.G > radiusPath)
            {
                break;
            }

            var walkable = !Physics2D.OverlapCircle(nodeToCheck.Position, 0.1f, SolidLayer);
            if (!walkable)
            {
                waitingNodes.Remove(nodeToCheck);
            }
            else if (walkable)
            {
                waitingNodes.Remove(nodeToCheck);
                if (!checkedNodes.Where(x => x.Position == nodeToCheck.Position).Any())
                {
                    checkedNodes.Add(nodeToCheck);
                    waitingNodes.AddRange(GetNeighbourNodes(nodeToCheck));
                }
            }
        }
        foreach (var node in checkedNodes)
        {
            freeNodes.Add(node.Position);
        }
        return freeNodes;
    }

    public List<Vector2> GetPath(Vector2 target)
    {
        var PathToTarget = new List<Vector2>();
        checkedNodes = new List<Node>();
        waitingNodes = new List<Node>();

        Vector2 StartPosition = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        Vector2 TargetPosition = new Vector2(Mathf.Round(target.x), Mathf.Round(target.y));
        
        if(StartPosition == TargetPosition) return PathToTarget;

        Node startNode = new Node(0, StartPosition, TargetPosition, null);
        checkedNodes.Add(startNode);
        waitingNodes.AddRange(GetNeighbourNodes(startNode));

        while (waitingNodes.Count > 0)
        {
            Node nodeToCheck = waitingNodes.Where(x => x.F == waitingNodes.Min(y => y.F)).FirstOrDefault();

            if (nodeToCheck.Position == TargetPosition)
            {
                return CalculatePathFromNode(nodeToCheck);
            }

            var walkable = !Physics2D.OverlapCircle(nodeToCheck.Position, 0.1f, SolidLayer);
            if(!walkable)
            {
                waitingNodes.Remove(nodeToCheck);
            }
            else if(walkable)
            {
                waitingNodes.Remove(nodeToCheck);
                if(!checkedNodes.Where(x => x.Position == nodeToCheck.Position).Any()) {
                    checkedNodes.Add(nodeToCheck);
                    waitingNodes.AddRange(GetNeighbourNodes(nodeToCheck));
                }
            }
        }
        return PathToTarget;
    }

    List<Vector2> CalculatePathFromNode(Node node)
    {
        var path = new List<Vector2>();
        Node currentNode = node;

        while(currentNode.PreviousNode != null)
        {
            path.Add(new Vector2(currentNode.Position.x, currentNode.Position.y));
            currentNode = currentNode.PreviousNode;
        }

        return path;
    }
    List<Node> GetNeighbourNodes (Node node)
    {
        var Neighbours = new List<Node>();

        Neighbours.Add(new Node(node.G + 1, new Vector2(
            node.Position.x-1, node.Position.y), 
            node.TargetPosition, 
            node));
        Neighbours.Add(new Node(node.G + 1, new Vector2(
            node.Position.x+1, node.Position.y),
            node.TargetPosition,
            node));
        Neighbours.Add(new Node(node.G + 1, new Vector2(
            node.Position.x, node.Position.y-1),
            node.TargetPosition,
            node));
        Neighbours.Add(new Node(node.G + 1, new Vector2(
            node.Position.x, node.Position.y+1),
            node.TargetPosition,
            node));
        return Neighbours;
    }

    void OnDrawGizmos()
    {
        foreach (var item in freeNodes)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(new Vector2(item.x, item.y), 0.1f);
        }
    }
}

[System.Serializable]
public class Node 
{
    public Vector2 Position;
    public Vector2 TargetPosition;
    public Node PreviousNode;
    public int F; // F=G+H
    public int G; // distância do início ao nó
    public int H; // distância do nó ao alvo

    public Node(int g, Vector2 nodePosition, Vector2 targetPosition, Node previousNode)
    {
        Position = nodePosition;
        TargetPosition = targetPosition;
        PreviousNode = previousNode;
        G = g;
        H = (int)Mathf.Abs(targetPosition.x - Position.x) + (int)Mathf.Abs(targetPosition.y - Position.y);
        F = G + H;
    }

    public Node(int g, Vector2 nodePosition, Node previousNode)
    {
        Position = nodePosition;
        PreviousNode = previousNode;
        G = g;
    }
}