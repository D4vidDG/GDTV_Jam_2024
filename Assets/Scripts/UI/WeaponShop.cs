using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] WeaponByShopItem[] weaponsByItem;
    [SerializeField] GameObject panel;
    WeaponInventory weaponInventory;
    [Serializable]
    struct WeaponByShopItem
    {
        public ShopItem shopItem;
        public Weapon weaponPrefab;
    }

    Dictionary<ShopItem, Weapon> lookUpTable;
    bool opened;

    private void Awake()
    {
        lookUpTable = new Dictionary<ShopItem, Weapon>();
        foreach (WeaponByShopItem item in weaponsByItem)
        {
            lookUpTable.Add(item.shopItem, item.weaponPrefab);
        }
        weaponInventory = FindObjectOfType<WeaponInventory>();
    }

    private void Start()
    {
        Close();
    }

    private void OnEnable()
    {
        foreach (WeaponByShopItem item in weaponsByItem)
        {
            item.shopItem.OnItemSold += OnItemSold;
        }
    }

    private void OnDisable()
    {

        foreach (WeaponByShopItem item in weaponsByItem)
        {
            item.shopItem.OnItemSold -= OnItemSold;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (opened) Close();
            else Open();
        }
    }

    public void Open()
    {
        panel.SetActive(true);
        opened = true;
    }

    public void Close()
    {
        panel.SetActive(false);
        opened = false;
    }


    public void NextWaveSignal()
    {
        GameManager.instance.NextWave();
    }

    private void OnItemSold(ShopItem item)
    {
        Weapon weaponPrefab = lookUpTable[item];
        Weapon weaponInstance = Instantiate<Weapon>(weaponPrefab);
        weaponInventory.AddWeapon(weaponInstance);
        weaponInventory.EquipWeapon(weaponInstance.GetWeaponType());
    }
}
