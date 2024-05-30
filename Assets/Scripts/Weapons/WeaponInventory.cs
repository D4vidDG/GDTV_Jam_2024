using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField] Transform gunHoldingPoint;

    int currentWeaponIndex;
    List<Weapon> weapons;
    public Weapon currentWeapon => _currentWeapon;
    Weapon _currentWeapon = null;

    private void Awake()
    {
        weapons = new List<Weapon>();
    }

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (HasWeapon(weapon))
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.UnEquip();
                currentWeapon.gameObject.SetActive(false);
            }
            weapon.gameObject.SetActive(true);
            weapon.Equip(gunHoldingPoint);
            currentWeaponIndex = weapons.IndexOf(weapon);
            _currentWeapon = weapons[currentWeaponIndex];
        }
    }

    public bool HasWeapon(Weapon weapon)
    {
        return weapons.Contains(weapon);
    }

    public void SwitchToNextWeapon()
    {
        if (weapons.Count > 1)
        {
            currentWeapon.UnEquip();
            currentWeapon.gameObject.SetActive(false);

            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
            _currentWeapon = weapons[currentWeaponIndex];

            currentWeapon.gameObject.SetActive(true);
            currentWeapon.Equip(gunHoldingPoint);
        }
    }

}