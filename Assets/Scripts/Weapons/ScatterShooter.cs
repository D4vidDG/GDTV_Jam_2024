using ExtensionMethods;
using UnityEngine;
public class ScatterShooter : Weapon
{
    [SerializeField] int numberOfBullets; // Total bullets show per Shot of the gun
    [SerializeField] float angleSpread; // Degrees (0-360) to spread the Bullets

    const int SHOTGUN_PENETRATION = 1;

    protected override void Fire(Vector2 shootingDirection)
    {
        if (numberOfBullets < 1 || angleSpread == 0) return;

        shootingDirection = shootingDirection.normalized;
        float deltaAngle = angleSpread / numberOfBullets;

        Vector2 oldDirection = shootingDirection.Rotate((angleSpread + deltaAngle) / 2);
        Vector2 newDirection;

        for (int i = 0; i < numberOfBullets; i++)
        {
            newDirection = oldDirection.Rotate(-deltaAngle);
            Debug.DrawRay(transform.position, newDirection * range, Color.yellow, 0.2f);
            LaunchProjectle(newDirection);
            oldDirection = newDirection;
        }
    }

    protected override void DrawGizmos()
    {
        base.DrawGizmos();
        Gizmos.color = Color.blue;

        float deltaAngle = angleSpread / numberOfBullets;
        Vector2 right = (Vector2)transform.right;
        Vector2 oldDirection = right.Rotate((angleSpread + deltaAngle) / 2);
        Vector2 newDirection;

        for (int i = 0; i < numberOfBullets; i++)
        {
            newDirection = oldDirection.Rotate(-deltaAngle);
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + newDirection * range);
            oldDirection = newDirection;
        }
    }
}

