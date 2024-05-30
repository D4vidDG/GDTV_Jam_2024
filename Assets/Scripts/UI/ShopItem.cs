using System;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    const string SOLD_TAG = "Sold";
    [SerializeField] int price;

    ShopButton shopButton;
    CoinWallet coinWallet;

    public Action<ShopItem> OnItemSold;
    bool itemSold;

    private void Awake()
    {
        coinWallet = FindObjectOfType<CoinWallet>();
        shopButton = GetComponentInChildren<ShopButton>();
    }


    private void Start()
    {
        itemSold = false;
        shopButton.SetPriceTag(price.ToString());
    }

    public void SellItem()
    {
        itemSold = true;
        shopButton.SetPriceTag(SOLD_TAG);
        shopButton.Disable();
        coinWallet.Spend(price);
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
