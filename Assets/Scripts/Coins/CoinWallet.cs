using UnityEngine;

public class CoinWallet : MonoBehaviour
{
    [SerializeField] int maxCoins;

    int coinsCollected;

    private void Start()
    {
        coinsCollected = 0;
    }

    public int GetCoinsCollected()
    {
        return coinsCollected;
    }

    public void Spend(int amount)
    {
        if (amount < coinsCollected) coinsCollected -= amount;
    }

    public void Reset()
    {
        coinsCollected = 0;
    }

    private void OnEnable()
    {
        Coin.OnCoinCollected += OnCoinCollected;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= OnCoinCollected;
    }

    private void OnCoinCollected()
    {
        coinsCollected++;
        coinsCollected = Mathf.Min(maxCoins, coinsCollected);
    }
}
