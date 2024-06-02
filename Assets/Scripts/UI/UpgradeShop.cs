using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.TMP_Dropdown;

public class UpgradeShop : UIShop
{
    [SerializeField] WeaponOptionData[] weaponOptionsData;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] TextMeshProUGUI noWeaponsText;
    [SerializeField] GameObject content;
    [SerializeField] StatText[] statsTexts;


    WeaponInventory weaponInventory;
    Dictionary<WeaponStat, StatText> statTextLookUpTable;
    Dictionary<WeaponType, OptionData> optionByWeaponType;
    Dictionary<OptionData, WeaponType> weaponTypeByOption;

    [Serializable]
    struct WeaponOptionData
    {
        public WeaponType weaponType;
        public OptionData dropDownOption;
    }

    protected override void Initialize()
    {
        statTextLookUpTable = new Dictionary<WeaponStat, StatText>();
        foreach (StatText statText in statsTexts)
        {
            statTextLookUpTable.Add(statText.stat, statText);
            statText.diff.enabled = false;
        }

        weaponTypeByOption = new Dictionary<OptionData, WeaponType>();
        optionByWeaponType = new Dictionary<WeaponType, OptionData>();
        foreach (WeaponOptionData weaponOption in weaponOptionsData)
        {
            weaponTypeByOption.Add(weaponOption.dropDownOption, weaponOption.weaponType);
            optionByWeaponType.Add(weaponOption.weaponType, weaponOption.dropDownOption);
        }

        weaponInventory = FindObjectOfType<WeaponInventory>();
    }

    public override void Open()
    {
        base.Open();
        UpdateDisplay();
    }

    protected override void OnItemSold(ShopItem item)
    {
        Upgrade upgrade = item as Upgrade;
        if (upgrade != null)
        {
            WeaponStat targetStat = upgrade.GetTargetStat();
            WeaponStats stats = GetSelectedWeaponStats();
            stats.IncreaseStatLevel(targetStat);
            UpdateDisplay();
        }
    }

    public void OnPointerEnterUpgrade(Upgrade upgrade)
    {
        WeaponStat targetStat = upgrade.GetTargetStat();
        WeaponStats stats = GetSelectedWeaponStats();
        StatText statText = statTextLookUpTable[targetStat];
        statText.diff.enabled = true;
        UpdateStatTextDiff(targetStat, stats);
    }



    public void OnPointerExitUpgrade(Upgrade upgrade)
    {
        WeaponStats stats = GetSelectedWeaponStats();
        WeaponStat targetStat = upgrade.GetTargetStat();
        StatText statText = statTextLookUpTable[targetStat];
        statText.diff.enabled = false;
    }

    public void UpdateDisplay()
    {
        UpdateDropdown();

        if (AnyDropdownItems())
        {
            content.SetActive(true);
            noWeaponsText.gameObject.SetActive(false);
            UpdateStatDisplay();
            UpdateUpgrades();
        }
        else
        {
            content.SetActive(false);
            noWeaponsText.gameObject.SetActive(true);
            return;
        }
    }

    private void UpdateUpgrades()
    {
        foreach (ShopItem item in items)
        {
            Upgrade upgrade = item as Upgrade;
            if (upgrade == null) continue;
            WeaponStats weaponStats = GetSelectedWeaponStats();
            if (weaponStats.IsAtMaxLevel(upgrade.GetTargetStat()))
            {
                upgrade.DisableSelling();
            }
            else
            {
                upgrade.EnableSelling();
            }
        }
    }

    private bool AnyDropdownItems()
    {
        return dropdown.options.Count > 0;
    }

    private void UpdateDropdown()
    {
        foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
        {
            OptionData option = optionByWeaponType[weaponType];
            bool optionNotOnDropdown = !dropdown.options.Contains(option);

            if (weaponInventory.HasWeapon(weaponType) && optionNotOnDropdown)
            {
                dropdown.options.Add(option);
            }
        }
    }

    void UpdateStatDisplay()
    {
        WeaponStats stats = GetSelectedWeaponStats();

        foreach (WeaponStat weaponStat in Enum.GetValues(typeof(WeaponStat)))
        {
            float statValue = stats.GetStat(weaponStat);
            statTextLookUpTable[weaponStat].value.text = statValue.ToString();
            UpdateStatTextDiff(weaponStat, stats);
        }

    }

    private void UpdateStatTextDiff(WeaponStat stat, WeaponStats weaponStats)
    {
        StatText statText = statTextLookUpTable[stat];
        bool isStatAtMaxLevel = weaponStats.IsAtMaxLevel(stat);
        if (!isStatAtMaxLevel)
        {
            int currentStatLevel = weaponStats.GetStatLevel(stat);

            float statDifference = weaponStats.GeStatAtLevel(stat, currentStatLevel + 1)
            - weaponStats.GeStatAtLevel(stat, currentStatLevel);

            statText.diff.text = "+" + statDifference.ToString();
        }
        else
        {
            statText.diff.text = "MAX";
        }
    }

    private WeaponStats GetSelectedWeaponStats()
    {
        return weaponInventory.GetWeapon(GetWeaponSelected()).stats;
    }

    private WeaponType GetWeaponSelected()
    {
        int dropdownIndex = dropdown.value;
        OptionData optionObject = dropdown.options[dropdownIndex];
        return weaponTypeByOption[optionObject];
    }


}

