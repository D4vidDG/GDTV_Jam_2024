using UnityEngine;

public class StraightShooter : Weapon
{
    protected override void Fire(Vector2 shootingDirection)
    {
        LaunchProjectle(shootingDirection);
        Debug.DrawRay(transform.position, shootingDirection * range, Color.yellow, 0.2f);
    }
}
