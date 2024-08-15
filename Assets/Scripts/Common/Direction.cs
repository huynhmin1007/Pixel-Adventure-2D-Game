using UnityEngine;

namespace Assets.Scripts.Common
{
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }

    public static class DirectionExtension
    {
        public static Vector2 ToVector2(this Direction direction)
        {
            return direction switch
            {
                Direction.LEFT => Vector2.left,
                Direction.RIGHT => Vector2.right,
                Direction.UP => Vector2.up,
                Direction.DOWN => Vector2.down,
                _ => throw new System.ArgumentOutOfRangeException(nameof(direction), $"Invalid direction value: {direction}")
            };
        }


        public static int XValue(this Direction direction)
        {
            return (int)direction.ToVector2().x;
        }

        public static int YValue(this Direction direction)
        {
            return (int)direction.ToVector2().y;
        }

        public static Direction Flip(this Direction direction)
        {
            return direction switch
            {
                Direction.LEFT => Direction.RIGHT,
                Direction.RIGHT => Direction.LEFT,
                Direction.UP => Direction.DOWN,
                Direction.DOWN => Direction.UP,
                _ => throw new System.ArgumentOutOfRangeException(nameof(direction), "Invalid direction value")
            };
        }
    }
}
