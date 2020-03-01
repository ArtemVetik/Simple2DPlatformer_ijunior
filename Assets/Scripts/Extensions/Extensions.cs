using UnityEngine;

static class Extensions
{
    public static MoveDirection Flip(this MoveDirection direction)
    {
        if (direction == MoveDirection.None)
            return direction;

        return (MoveDirection)((int)direction * -1);
    }

    public static Vector2 ToVector2(this MoveDirection direction)
    {
        return new Vector2((int)direction, 0);
    }
}

