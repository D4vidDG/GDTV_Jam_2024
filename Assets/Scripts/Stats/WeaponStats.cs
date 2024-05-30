using System;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{

    [Range(1, 99)][SerializeField] int startingLevel;
    [SerializeField] WeaponProgression progressionData = null;

    WeaponType weaponType;

    public Action OnLevelUp;

    int currentLevel = 1;

    private void Start()
    {
        weaponType = GetComponent<Weapon>().GetWeaponType();
        currentLevel = startingLevel;
    }

    public float GetStat(WeaponStat stat)
    {
        return progressionData.GetStat(stat, weaponType, currentLevel);
    }

    public float GeStatAtLevel(WeaponStat stat, int level)
    {
        return progressionData.GetStat(stat, weaponType, level);
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    public void IncreaseLevel()
    {
        currentLevel++;
    }

    public void DecreaseLevel()
    {
        currentLevel--;
    }
}

