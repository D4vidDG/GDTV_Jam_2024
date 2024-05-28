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
    Dictionary<string, Sprite> iconByWeapon;



    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        iconByWeapon = new Dictionary<string, Sprite>();

        foreach (AmmoIconByWeapon icon in icons)
        {
            iconByWeapon.Add(GetWeaponID(icon.weapon), icon.ammoIcon);
        }
    }


    private void Update()
    {
        ammoText.text = "X" + player.currentWeapon.GetAmmoLeft().ToString();
        ammoImage.sprite = iconByWeapon[GetWeaponID(player.currentWeapon)];
    }


    private string GetWeaponID(Weapon weapon)
    {
        return weapon.GetType().Name;
    }

    [Serializable]
    struct AmmoIconByWeapon
    {
        public Weapon weapon;
        public Sprite ammoIcon;
    }

}
