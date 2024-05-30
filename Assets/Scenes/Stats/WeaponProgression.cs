
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New  Progression", order = 0)]
public class WeaponProgression : ScriptableObject
{
    [SerializeField] WeaponStatsProgression[] weaponsProgression;
    Dictionary<WeaponType, Dictionary<WeaponStat, float[]>> lookUpTable;

    [System.Serializable]
    struct WeaponStatsProgression
    {
        public WeaponType weaponType;
        public ProgressionStat[] weaponStats;
    }

    [System.Serializable]
    struct ProgressionStat
    {
        public WeaponStat stat;
        public float[] levels;
    }

    public float GetStat(WeaponStat stat, WeaponType weaponType, int level)
    {
        BuildLookUpTable();
        if (!lookUpTable[weaponType].ContainsKey(stat)) return -1;
        float[] statLevels = lookUpTable[weaponType][stat];
        if (statLevels.Length < level)
        {
            return -1;
        }
        return lookUpTable[weaponType][stat][level - 1];
    }

    private void BuildLookUpTable()
    {
        if (lookUpTable != null) return;

        lookUpTable = new Dictionary<WeaponType, Dictionary<WeaponStat, float[]>>();

        for (int i = 0; i < weaponsProgression.Length; i++)
        {
            Dictionary<WeaponStat, float[]> weaponStatsTable = new Dictionary<WeaponStat, float[]>();
            WeaponStatsProgression currentWeaponProgression = weaponsProgression[i];
            for (int j = 0; j < currentWeaponProgression.weaponStats.Length; j++)
            {
                ProgressionStat currentStatProgression = currentWeaponProgression.weaponStats[j];
                weaponStatsTable.Add(currentStatProgression.stat, currentStatProgression.levels);
            }

            lookUpTable.Add(currentWeaponProgression.weaponType, weaponStatsTable);
        }
    }
}