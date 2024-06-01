using UnityEngine;
public class Upgrade : ShopItem
{
    [SerializeField] WeaponStat targetStat;


    public WeaponStat GetTargetStat()
    {
        return targetStat;
    }
}
