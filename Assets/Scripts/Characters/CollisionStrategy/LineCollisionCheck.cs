using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Characters.CollisionStrategy
{
    [System.Serializable]
    public class LineCollisionCheck
    {
        [SerializeField] private Transform checker;
        [SerializeField] private float distance;
        [SerializeField] private Direction direction;

        public Direction Direction { get => direction; set => direction = value; }

        public RaycastHit2D Check(LayerMask layerCheck)
        {
            return Physics2D.Raycast(checker.position, direction.ToVector2(), distance, layerCheck);
        }

        public void Draw(Color color = default)
        {
            if (checker == null)
                return;

            color = color == default ? Color.cyan : color;

            Gizmos.color = color;
            Vector2 directionVector = direction.ToVector2();
            Vector3 endPoint = checker.position + (Vector3)directionVector * distance;

            Gizmos.DrawLine(checker.position, endPoint);
        }
    }
}
