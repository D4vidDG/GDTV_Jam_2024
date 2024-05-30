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

    PlayerController player;
    Dictionary<WeaponType, Sprite> iconByWeapon;



    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        iconByWeapon = new Dictionary<WeaponType, Sprite>();

        foreach (AmmoIconByWeapon icon in icons)
        {
            iconByWeapon.Add(icon.weaponType, icon.ammoIcon);
        }
    }


    private void Update()
    {
        ammoText.text = "X" + player.currentWeapon.GetAmmoLeft().ToString();
        ammoImage.sprite = iconByWeapon[player.currentWeapon.GetWeaponType()];
    }


    [Serializable]
    struct AmmoIconByWeapon
    {
        public WeaponType weaponType;
        public Sprite ammoIcon;
    }

}
