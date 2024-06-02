using UnityEngine;
public class Upgrade : ShopItem
{
    [SerializeField] WeaponStat targetStat;


    public WeaponStat GetTargetStat()
    {
        return targetStat;
    }

    public void EnableSelling()
    {
        collectCoins = false;
        shopButton.Enable();
    }

    public void DisableSelling()
    {
        collectCoins = true;
        shopButton.Disable();
    }

    public override void SellItem()
    {
        coinWallet.Spend(price);
        OnItemSold?.Invoke(this);
    }
}
