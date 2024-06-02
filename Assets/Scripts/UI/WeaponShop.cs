using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : UIShop
{
    [SerializeField] WeaponByShopItem[] weaponsByItem;
    WeaponInventory weaponInventory;
    [Serializable]
    struct WeaponByShopItem
    {
        public ShopItem shopItem;
        public Weapon weaponPrefab;
    }

    Dictionary<ShopItem, Weapon> lookUpTable;

    protected override void Initialize()
    {
        lookUpTable = new Dictionary<ShopItem, Weapon>();
        foreach (WeaponByShopItem item in weaponsByItem)
        {
            lookUpTable.Add(item.shopItem, item.weaponPrefab);
        }
        weaponInventory = FindObjectOfType<WeaponInventory>();
    }


    protected override void OnItemSold(ShopItem item)
    {
        Weapon weaponPrefab = lookUpTable[item];
        Weapon weaponInstance = Instantiate<Weapon>(weaponPrefab);
        weaponInventory.AddWeapon(weaponInstance);
        weaponInventory.EquipWeapon(weaponInstance.GetWeaponType());
    }
}
