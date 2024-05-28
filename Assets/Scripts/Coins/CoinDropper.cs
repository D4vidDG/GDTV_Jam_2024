using ExtensionMethods;
using UnityEngine;

public class CoinDropper : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] int numberOfCoins;
    [SerializeField] Transform dropPoint;
    [SerializeField] Coin coinPrefab;

    public void DropCoins()
    {
        Vector2 upDirection = transform.up;
        float deltaAngle = 360f / numberOfCoins;
        for (int i = 0; i < numberOfCoins; i++)
        {
            Coin coin = Instantiate<Coin>(coinPrefab, dropPoint.position, Quaternion.identity, null);
            Vector2 launchDirection = upDirection.Rotate(deltaAngle * i).normalized;
            coin.GetComponent<Rigidbody2D>().AddForce(launchDirection * force, ForceMode2D.Impulse);
        }
    }
}
