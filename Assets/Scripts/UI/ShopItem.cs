using System;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    const string SOLD_TAG = "Sold";
    [SerializeField] protected int price;
    [SerializeField] bool sellIndefinitely;
    [SerializeField] bool soldOnStart;

    protected ShopButton shopButton;
    protected CoinWallet coinWallet;

    public Action<ShopItem> OnItemSold;
    protected bool itemSold;

    private void Awake()
    {
        coinWallet = FindObjectOfType<CoinWallet>();
        shopButton = GetComponentInChildren<ShopButton>();
    }


    private void Start()
    {
        if (soldOnStart)
        {
            itemSold = true;
            shopButton.SetPriceTag(SOLD_TAG);
            shopButton.Disable();
        }
        else
        {
            itemSold = false;
            shopButton.SetPriceTag(price.ToString());
        }
    }

    public void SellItem()
    {
        coinWallet.Spend(price);
        if (!sellIndefinitely)
        {
            itemSold = true;
            shopButton.SetPriceTag(SOLD_TAG);
            shopButton.Disable();
        }
        OnItemSold?.Invoke(this);
    }

    private void Update()
    {
        if (itemSold) return;
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
