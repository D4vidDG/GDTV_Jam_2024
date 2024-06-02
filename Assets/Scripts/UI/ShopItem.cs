using System;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    const string SOLD_TAG = "Sold";
    [SerializeField] protected int price;
    [SerializeField] bool soldOnStart;

    protected ShopButton shopButton;
    protected CoinWallet coinWallet;

    public Action<ShopItem> OnItemSold;
    protected bool collectCoins;

    private void Awake()
    {
        coinWallet = FindObjectOfType<CoinWallet>();
        shopButton = GetComponentInChildren<ShopButton>();
    }


    private void Start()
    {
        if (soldOnStart)
        {
            collectCoins = true;
            shopButton.SetPriceTag(SOLD_TAG);
            shopButton.Disable();
        }
        else
        {
            collectCoins = false;
            shopButton.SetPriceTag(price.ToString());
        }
    }

    public virtual void SellItem()
    {
        coinWallet.Spend(price);
        collectCoins = true;
        shopButton.SetPriceTag(SOLD_TAG);
        shopButton.Disable();
        OnItemSold?.Invoke(this);
    }

    private void Update()
    {
        if (collectCoins) return;
        int coinsCollected = coinWallet.GetCoinsCollected();
        if (price <= coinsCollected)
        {
            shopButton.Enable();
        }
        else
        {
            shopButton.Disable();
        }
    }


}
