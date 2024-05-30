using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Image ammoImage;
    [SerializeField] AmmoIconByWeapon[] icons;

    WeaponInventory weaponInventory;
    Dictionary<WeaponType, Sprite> iconByWeapon;



    private void Awake()
    {
        weaponInventory = GameObject.FindWithTag("Player").GetComponent<WeaponInventory>();
        iconByWeapon = new Dictionary<WeaponType, Sprite>();

        foreach (AmmoIconByWeapon icon in icons)
        {
            iconByWeapon.Add(icon.weaponType, icon.ammoIcon);
        }
    }


    private void Update()
    {
        Weapon currentWeapon = weaponInventory.currentWeapon;
        if (currentWeapon == null)
        {
            ammoText.enabled = false;
            ammoImage.enabled = false;
        }
        else
        {
            ammoText.enabled = true;
            ammoImage.enabled = true;
            ammoText.text = "X" + currentWeapon.GetAmmoLeft().ToString();
            ammoImage.sprite = iconByWeapon[currentWeapon.GetWeaponType()];
        }
    }


    [Serializable]
    struct AmmoIconByWeapon
    {
        public WeaponType weaponType;
        public Sprite ammoIcon;
    }

}
