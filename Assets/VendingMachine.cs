using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public WeaponShop weaponShop;
    public UpgradeShop upgradeShop;
    public GameObject reminder;
    // Start is called before the first frame update
    void Start()
    {
        weaponShop = FindObjectOfType<WeaponShop>();
        upgradeShop = FindObjectOfType<UpgradeShop>();
        reminder.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            weaponShop.ToggleAccess(true);
            upgradeShop.ToggleAccess(true);
            reminder.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            weaponShop.ToggleAccess(false);
            upgradeShop.ToggleAccess(false);
            reminder.SetActive(false);
        }
    }
}
