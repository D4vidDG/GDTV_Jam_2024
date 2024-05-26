using ExtensionMethods;
using UnityEngine;

public class RandomSpread : Weapon
{
    [SerializeField] float maxRandomSpread;

    protected override void Fire(Vector2 shootingDirection)
    {
        if (maxRandomSpread == 0) return;

        shootingDirection = shootingDirection.normalized;

        float randomAngle = Random.Range(-maxRandomSpread, maxRandomSpread);
        Vector2 newDirection = shootingDirection.Rotate(randomAngle);

        LaunchProjectle(newDirection);
    }
}
