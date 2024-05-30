using UnityEngine;

public class CoinWallet : MonoBehaviour
{
    [SerializeField] int maxCoins;
    [SerializeField] int startCoins = 0;

    int coinsCollected;

    private void Start()
    {
        coinsCollected = startCoins;
    }

    public int GetCoinsCollected()
    {
        return coinsCollected;
    }

    public void Spend(int amount)
    {
        if (amount <= coinsCollected) coinsCollected -= amount;
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
