using UnityEngine;

public class StraightShooter : FirePattern
{
    public override Vector2[] GetDirections(Vector2 shootingDirection)
    {
        Vector2[] directions = new Vector2[1];
        directions[0] = shootingDirection.normalized;
        return directions;
    }
}
