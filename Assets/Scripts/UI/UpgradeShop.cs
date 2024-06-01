using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.TMP_Dropdown;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] WeaponOptionData[] weaponOptionsData;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] TextMeshProUGUI noWeaponsText;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject content;
    [SerializeField] StatText[] statsTexts;


    WeaponInventory weaponInventory;
    Dictionary<WeaponStat, StatText> statTextLookUpTable;
    Dictionary<WeaponType, OptionData> optionByWeaponType;
    Dictionary<OptionData, WeaponType> weaponTypeByOption;
    Upgrade[] upgrades;

    bool opened = false;


    [Serializable]
    struct WeaponOptionData
    {
        public WeaponType weaponType;
        public OptionData dropDownOption;
    }

    private void Awake()
    {
        statTextLookUpTable = new Dictionary<WeaponStat, StatText>();
        foreach (StatText statText in statsTexts)
        {
            statTextLookUpTable.Add(statText.stat, statText);
        }

        weaponTypeByOption = new Dictionary<OptionData, WeaponType>();
        optionByWeaponType = new Dictionary<WeaponType, OptionData>();
        foreach (WeaponOptionData weaponOption in weaponOptionsData)
        {
            weaponTypeByOption.Add(weaponOption.dropDownOption, weaponOption.weaponType);
            optionByWeaponType.Add(weaponOption.weaponType, weaponOption.dropDownOption);
        }

        upgrades = GetComponentsInChildren<Upgrade>(true);
        weaponInventory = FindObjectOfType<WeaponInventory>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (opened) Close();
            else Open();
        }
    }

    private void OnEnable()
    {
        foreach (Upgrade upgrade in upgrades)
        {
            upgrade.OnItemSold += OnUpgradeSold;
        }
    }

    private void OnDisable()
    {
        foreach (Upgrade upgrade in upgrades)
        {
            upgrade.OnItemSold -= OnUpgradeSold;
        }
    }

    public void Open()
    {
        UpdateDisplay();
        opened = true;
        panel.SetActive(true);
    }

    public void Close()
    {
        opened = false;
        panel.SetActive(false);
    }

    private void OnUpgradeSold(ShopItem item)
    {
        Upgrade upgrade = item as Upgrade;
        if (upgrade != null)
        {
            WeaponType weaponSelected = GetWeaponSelected();
            WeaponStat targetStat = upgrade.GetTargetStat();
            WeaponStats weaponStats = weaponInventory.GetWeapon(weaponSelected).stats;
            weaponStats.IncreaseStatLevel(targetStat);
            UpdateStatDisplay();
        }
    }

    public void OnPointerEnterUpgrade(Upgrade upgrade)
    {
        WeaponType weaponSelected = GetWeaponSelected();
        WeaponStat targetStat = upgrade.GetTargetStat();

        WeaponStats stats = weaponInventory.GetWeapon(weaponSelected).stats;
        int currentStatLevel = stats.GetStatLevel(targetStat);

        StatText statText = statTextLookUpTable[targetStat];
        float statDifference = stats.GeStatAtLevel(targetStat, currentStatLevel + 1)
        - stats.GeStatAtLevel(targetStat, currentStatLevel);

        statText.diff.text = "+" + statDifference.ToString();
        statText.diff.enabled = true;
    }

    public void OnPointerExitUpgrade(Upgrade upgrade)
    {
        WeaponStat targetStat = upgrade.GetTargetStat();
        StatText statText = statTextLookUpTable[targetStat];
        statText.diff.enabled = false;
    }

    public void UpdateDisplay()
    {
        UpdateDropdown();
        UpdateStatDisplay();

        if (AnyWeaponsUnlocked())
        {
            content.SetActive(false);
            noWeaponsText.gameObject.SetActive(true);
            return;
        }
        else
        {
            content.SetActive(true);
            noWeaponsText.gameObject.SetActive(false);
        }
    }


    private bool AnyWeaponsUnlocked()
    {
        return dropdown.options.Count < 1;
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
        if (dropdown.options.Count < 1) return;

        Weapon weaponObject = null;
        WeaponType weaponSelected = GetWeaponSelected();

        if (weaponInventory.HasWeapon(weaponSelected))
        {
            weaponObject = weaponInventory.GetWeapon(weaponSelected);
        }
        else
        {
            return;
        }

        foreach (WeaponStat weaponStat in Enum.GetValues(typeof(WeaponStat)))
        {
            float statValue = weaponObject.stats.GetStat(weaponStat);
            statTextLookUpTable[weaponStat].value.text = statValue.ToString();
        }
    }

    private WeaponType GetWeaponSelected()
    {
        int dropdownIndex = dropdown.value;
        OptionData optionObject = dropdown.options[dropdownIndex];
        return weaponTypeByOption[optionObject];
    }

}

