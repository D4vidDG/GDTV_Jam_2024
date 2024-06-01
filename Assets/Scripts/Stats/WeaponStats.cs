using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{

    [Range(1, 99)][SerializeField] int startingLevel;
    [SerializeField] WeaponProgression progressionData = null;

    Dictionary<WeaponStat, int> statLevels;
    WeaponType weaponType;

    public Action OnLevelUp;

    int currentLevel = 1;
    private void Awake()
    {
        statLevels = new Dictionary<WeaponStat, int>();
        foreach (WeaponStat stat in Enum.GetValues(typeof(WeaponStat)))
        {
            statLevels[stat] = startingLevel;
        }
        weaponType = GetComponent<Weapon>().GetWeaponType();
    }


    private void Start()
    {
        currentLevel = startingLevel;
    }

    public float GetStat(WeaponStat stat)
    {
        return progressionData.GetStat(stat, weaponType, statLevels[stat]);
    }

    public float GeStatAtLevel(WeaponStat stat, int level)
    {
        return progressionData.GetStat(stat, weaponType, level);
    }

    public int GetStatLevel(WeaponStat stat)
    {
        return statLevels[stat];
    }

    public void IncreaseStatLevel(WeaponStat stat)
    {
        statLevels[stat]++;
    }

    public void DecreaseStatLevel(WeaponStat stat)
    {
        statLevels[stat]++;
    }
}

