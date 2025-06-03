using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.TMP_Dropdown;

public class StatUpgradeShop : UIShop
{
    [SerializeField] WeaponDropdownOption[] weaponDropdownOptions;
    [SerializeField] StatUpgradeData[] statUpgradesData;
    [SerializeField] TMP_Dropdown weaponSelectionDropdown;
    [SerializeField] TextMeshProUGUI noWeaponsText;
    [SerializeField] GameObject content;
    [SerializeField] StatUI[] statsUI;


    Dictionary<WeaponType, OptionData> dropdownOptionByWeaponType;
    Dictionary<OptionData, WeaponType> weaponTypeByDropdownOption;
    Dictionary<ShopItem, WeaponStat> targetStatByShopItem;
    Dictionary<WeaponStat, StatUI> statUIByStatType;

    WeaponInventory weaponInventory;

    int NumberOfWeaponsInDropdown => weaponSelectionDropdown.options.Count;

    [Serializable]
    struct WeaponDropdownOption
    {
        public WeaponType weaponType;
        public OptionData optionData;
    }


    [Serializable]
    struct StatUpgradeData
    {
        public ShopItem shopItem;
        public WeaponStat targetStat;
    }

    protected override void Initialize()
    {

        weaponTypeByDropdownOption = new Dictionary<OptionData, WeaponType>();
        dropdownOptionByWeaponType = new Dictionary<WeaponType, OptionData>();
        foreach (WeaponDropdownOption weaponOption in weaponDropdownOptions)
        {
            weaponTypeByDropdownOption.Add(weaponOption.optionData, weaponOption.weaponType);
            dropdownOptionByWeaponType.Add(weaponOption.weaponType, weaponOption.optionData);
        }

        statUIByStatType = new Dictionary<WeaponStat, StatUI>();
        foreach (StatUI statUI in statsUI)
        {
            statUIByStatType.Add(statUI.targetStat, statUI);
            statUI.increase.enabled = false;
        }

        targetStatByShopItem = new Dictionary<ShopItem, WeaponStat>();
        foreach (StatUpgradeData statUpgradeData in statUpgradesData)
        {
            targetStatByShopItem.Add(statUpgradeData.shopItem, statUpgradeData.targetStat);
        }

        weaponInventory = FindObjectOfType<WeaponInventory>();
    }

    protected override void OnItemSold(ShopItem item)
    {
        WeaponStat targetStat = targetStatByShopItem[item];
        WeaponStats selectedWeaponStats = GetSelectedWeaponStats();
        selectedWeaponStats.IncreaseStatLevel(targetStat);

        UpdateDisplay();

        AudioManager.instance.PlaySound(SoundName.Upgrade);
    }

    public void OnPointerEnterUpgrade(ShopItem item)
    {
        //when mouse hovers over an upgrade, show stat increase
        WeaponStat targetStat = targetStatByShopItem[item];
        StatUI statUI = statUIByStatType[targetStat];
        statUI.increase.enabled = true;
        UpdateStatIncreaseText(targetStat);
    }

    public void OnPointerExitUpgrade(ShopItem item)
    {
        //when mouse doesn't hover over an upgrade, hide stat increase
        WeaponStat targetStat = targetStatByShopItem[item];
        StatUI statUI = statUIByStatType[targetStat];
        statUI.increase.enabled = false;
    }

    public override void Open()
    {
        base.Open();
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        UpdateWeaponSelectionDropdown();

        //if there's options in dropdown
        if (NumberOfWeaponsInDropdown > 0)
        {
            //enable shop content
            content.SetActive(true);
            noWeaponsText.gameObject.SetActive(false);
            UpdateStatsDisplay();
            UpdateShopItems();
        }
        else
        {
            //disable shop content
            content.SetActive(false);
            noWeaponsText.gameObject.SetActive(true);
        }
    }

    private void UpdateWeaponSelectionDropdown()
    {
        //for every weapon type
        foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
        {
            //check if weapon is on dropdown
            OptionData weaponDropdownOption = dropdownOptionByWeaponType[weaponType];
            bool weaponOnDropdown = weaponSelectionDropdown.options.Contains(weaponDropdownOption);
            //if the player carries the weapon and the weapon is not on dropdown
            if (weaponInventory.HasWeapon(weaponType) && !weaponOnDropdown)
            {
                //add weapon to dropdown
                weaponSelectionDropdown.options.Add(weaponDropdownOption);
            }
        }
    }

    void UpdateStatsDisplay()
    {
        WeaponStats weaponStats = GetSelectedWeaponStats();

        foreach (WeaponStat statType in Enum.GetValues(typeof(WeaponStat)))
        {
            float statCurrentValue = weaponStats.GetStat(statType);
            StatUI statUI = statUIByStatType[statType];
            statUI.value.text = statCurrentValue.ToString();
            UpdateStatIncreaseText(statType);
        }
    }


    private void UpdateStatIncreaseText(WeaponStat statType)
    {
        WeaponStats weaponStats = GetSelectedWeaponStats();
        StatUI statUI = statUIByStatType[statType];
        bool isStatAtMaxLevel = weaponStats.IsAtMaxLevel(statType);

        //if the stat is not at max level
        if (!isStatAtMaxLevel)
        {
            //show increase difference
            int currentStatLevel = weaponStats.GetStatLevel(statType);
            float currentValue = weaponStats.GeStatAtLevel(statType, currentStatLevel);
            float nextLevelValue = weaponStats.GeStatAtLevel(statType, currentStatLevel + 1);
            float statValueDifference = nextLevelValue - currentValue;

            statUI.increase.text = "+" + statValueDifference.ToString();
        }
        else
        {
            //show max level indicator 
            statUI.increase.text = "MAX";
        }
    }

    private void UpdateShopItems()
    {
        //for every stat upgrade in the shop
        foreach (ShopItem item in shopItems)
        {
            WeaponStats weaponStats = GetSelectedWeaponStats();
            WeaponStat targetStat = targetStatByShopItem[item];
            //if stat is at max level
            if (weaponStats.IsAtMaxLevel(targetStat))
            {
                //stop selling stat upgrade
                item.DisableSelling();
            }
            else
            {
                //keep selling stat upgrade
                item.EnableSelling();
            }
        }
    }

    private WeaponStats GetSelectedWeaponStats()
    {

        WeaponType selectedWeaponType = GetSelectedWeapon();
        return weaponInventory.GetWeapon(selectedWeaponType).stats;
    }

    private WeaponType GetSelectedWeapon()
    {
        int dropdownIndex = weaponSelectionDropdown.value;
        OptionData optionObject = weaponSelectionDropdown.options[dropdownIndex];
        return weaponTypeByDropdownOption[optionObject];
    }
}

