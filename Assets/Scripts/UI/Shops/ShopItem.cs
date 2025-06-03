using System;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] int price;
    [SerializeField] bool soldOnStart;
    [SerializeField] bool oneTimePurchase;
    [SerializeField] string soldTag = "Sold";

    SaleButton saleButton;
    CoinWallet coinWallet;

    public Action<ShopItem> OnItemSold;
    bool canBuy = true;

    private void Awake()
    {
        coinWallet = FindObjectOfType<CoinWallet>();
        saleButton = GetComponentInChildren<SaleButton>();
    }


    private void Start()
    {
        if (soldOnStart)
        {
            saleButton.SetPriceTag(soldTag);
            DisableSelling();
        }
        else
        {
            saleButton.SetPriceTag(price.ToString());
            EnableSelling();
        }
    }

    public void EnableSelling()
    {
        canBuy = true;
    }

    public void DisableSelling()
    {
        canBuy = false;
        saleButton.Disable();
    }

    public virtual void SellItem()
    {
        coinWallet.Spend(price);

        if (oneTimePurchase)
        {
            saleButton.SetPriceTag(soldTag);
            DisableSelling();
        }

        OnItemSold?.Invoke(this);
    }

    private void Update()
    {
        if (!canBuy) return;
        int coinsCollected = coinWallet.GetCoinsCollected();
        if (price <= coinsCollected)
        {
            saleButton.Enable();
        }
        else
        {
            saleButton.Disable();
        }
    }


}
