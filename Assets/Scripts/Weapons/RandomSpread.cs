using ExtensionMethods;
using UnityEngine;

public class RandomSpread : FirePattern
{
    [SerializeField][Min(1)] int numberOfBullets;
    [SerializeField] float maxRandomSpread;

    public override Vector2[] GetDirections(Vector2 shootingDirection)
    {
        Vector2[] directions = new Vector2[numberOfBullets];
        for (int i = 0; i < numberOfBullets; i++)
        {
            float randomAngle = Random.Range(-maxRandomSpread, maxRandomSpread);
            Vector2 newDirection = shootingDirection.Rotate(randomAngle);
            directions[i] = newDirection.normalized;
        }
        return directions;
    }
}
