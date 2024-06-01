using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponVendingMachine : MonoBehaviour
{
    public WeaponShop weaponShop;
    public GameObject reminder;
    // Start is called before the first frame update
    void Start()
    {
        weaponShop = FindObjectOfType<WeaponShop>();
        reminder.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            weaponShop.ToggleAccess(true);
            reminder.SetActive(true);
            reminder.GetComponent<TextMeshProUGUI>().text = "'W' Weapons Shop";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            weaponShop.ToggleAccess(false);
            reminder.SetActive(false);
        }
    }
}
