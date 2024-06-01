using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] bool canAccessShop;
    public GameObject reminderGO;
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
        ToggleAccess(false);
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

    public void ToggleAccess(bool toggle)
    {
        canAccessShop = toggle;

        if (canAccessShop)
        {
            reminderGO.SetActive(true);
        }
        else
        {
            reminderGO.SetActive(false);
            Close();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canAccessShop)
        {
            if (opened) Close();
            else Open();
        }
    }

    public void Open()
    {
        GameManager.instance.ToggleControl(false);
        panel.SetActive(true);
        opened = true;
        reminderGO.SetActive(false);
    }

    public void Close()
    {
        GameManager.instance.ToggleControl(true);
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
