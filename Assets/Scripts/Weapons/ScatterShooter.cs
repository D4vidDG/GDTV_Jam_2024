using ExtensionMethods;
using UnityEngine;
public class ScatterShooter : FirePattern
{
    [SerializeField][Min(1)] int numberOfBullets; // Total bullets show per Shot of the gun
    [SerializeField] float angleSpread; // Degrees (0-360) to spread the Bullets

    public override Vector2[] GetDirections(Vector2 shootingDirection)
    {
        Vector2[] directions = new Vector2[numberOfBullets];
        if (numberOfBullets < 1 || angleSpread == 0) return directions;

        shootingDirection = shootingDirection.normalized;
        float deltaAngle = angleSpread / numberOfBullets;

        Vector2 oldDirection = shootingDirection.Rotate((angleSpread + deltaAngle) / 2);
        Vector2 newDirection;

        for (int i = 0; i < numberOfBullets; i++)
        {
            newDirection = oldDirection.Rotate(-deltaAngle);
            directions[i] = newDirection.normalized;
            oldDirection = newDirection;
        }

        return directions;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        float deltaAngle = angleSpread / numberOfBullets;
        Vector2 right = (Vector2)transform.right;
        Vector2 oldDirection = right.Rotate((angleSpread + deltaAngle) / 2);
        Vector2 newDirection;

        for (int i = 0; i < numberOfBullets; i++)
        {
            newDirection = oldDirection.Rotate(-deltaAngle);
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + newDirection * 5);
            oldDirection = newDirection;
        }
    }
}

