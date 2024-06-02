using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField] Weapon initialWeapon;
    [SerializeField] Transform gunHoldingPoint;

    int currentWeaponIndex;
    public Weapon currentWeapon => _currentWeapon;
    Weapon _currentWeapon = null;
    Dictionary<WeaponType, Weapon> weaponsDict;
    List<Weapon> weapons;

    private void Awake()
    {
        weaponsDict = new Dictionary<WeaponType, Weapon>();
        weapons = new List<Weapon>();
    }

    private void Start()
    {
        if (initialWeapon != null)
        {
            AddWeapon(initialWeapon);
            EquipWeapon(initialWeapon.GetWeaponType());
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        if (HasWeapon(weapon.GetWeaponType())) return;
        weaponsDict.Add(weapon.GetWeaponType(), weapon);
        weapons.Add(weapon);
    }

    public Weapon GetWeapon(WeaponType weaponType)
    {
        if (!HasWeapon(weaponType)) return null;

        return weaponsDict[weaponType];
    }

    public void EquipWeapon(WeaponType weaponType)
    {
        if (HasWeapon(weaponType))
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.UnEquip();
                currentWeapon.gameObject.SetActive(false);
            }

            Weapon weaponObject = weaponsDict[weaponType];
            weaponObject.gameObject.SetActive(true);
            weaponObject.Equip(gunHoldingPoint);

            currentWeaponIndex = weapons.IndexOf(weaponObject);
            _currentWeapon = weapons[currentWeaponIndex];
        }
    }

    public bool HasWeapon(WeaponType weaponType)
    {
        return weaponsDict.ContainsKey(weaponType);
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