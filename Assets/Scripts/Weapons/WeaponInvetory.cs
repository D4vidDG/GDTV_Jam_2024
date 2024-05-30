using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField] List<Weapon> weapons;
    [SerializeField] Transform gunHoldingPoint;

    Weapon activeWeapon;
    Weapon nextWeapon;

    int currentIndex;

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }


}