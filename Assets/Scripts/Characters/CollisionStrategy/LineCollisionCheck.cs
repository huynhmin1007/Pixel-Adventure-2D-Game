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
        public Transform Checker { get => checker; set => checker = value; }
        public float Distance { get => distance; set => distance = value; }

        public RaycastHit2D Check(LayerMask layerCheck)
        {
            return Physics2D.Raycast(Checker.position, direction.ToVector2(), Distance, layerCheck);
        }

        public void Draw(Color color = default)
        {
            if (Checker == null)
                return;

            color = color == default ? Color.cyan : color;

            Gizmos.color = color;
            Vector2 directionVector = direction.ToVector2();
            Vector3 endPoint = Checker.position + (Vector3)directionVector * Distance;

            Gizmos.DrawLine(Checker.position, endPoint);
        }
    }
}
