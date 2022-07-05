using UnityEngine;

namespace PlayerScripts
{
    public class Node
    {
        public Vector2 Position;
        public Vector2 TargetPosition;
        public Node PreviosNode;
        public float F;
        public float G;
        public float H;
        
        public Node(float g, Vector2 nodePosition, Vector2 targetPosition, Node previousNode)
        {
            Position = nodePosition;
            TargetPosition = targetPosition;
            PreviosNode = previousNode;
            G = g;
            H =  Mathf.Abs(targetPosition.x - nodePosition.x) +  Mathf.Abs(targetPosition.y - nodePosition.y);
            F = G + H;

        }
    }
}