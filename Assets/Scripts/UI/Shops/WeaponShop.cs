using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : UIShop
{
    [SerializeField] ShopItemData[] shopItemsData;
    WeaponInventory weaponInventory;

    [Serializable]
    struct ShopItemData
    {
        public ShopItem shopItem;
        public Weapon weaponPrefab;
    }

    Dictionary<ShopItem, Weapon> weaponByShopItem;

    protected override void Initialize()
    {
        weaponByShopItem = new Dictionary<ShopItem, Weapon>();
        foreach (ShopItemData itemData in shopItemsData)
        {
            weaponByShopItem.Add(itemData.shopItem, itemData.weaponPrefab);
        }
        weaponInventory = FindObjectOfType<WeaponInventory>();
    }


    protected override void OnItemSold(ShopItem item)
    {
        Weapon weaponPrefab = weaponByShopItem[item];
        Weapon weaponInstance = Instantiate<Weapon>(weaponPrefab);
        weaponInventory.AddWeapon(weaponInstance);
        weaponInventory.EquipWeapon(weaponInstance.GetWeaponType());
    }
}
