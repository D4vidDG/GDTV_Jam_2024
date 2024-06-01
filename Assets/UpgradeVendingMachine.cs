using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeVendingMachine : MonoBehaviour
{
    public UpgradeShop upgradeShop;
    public GameObject reminder;
    // Start is called before the first frame update
    void Start()
    {
        upgradeShop = FindObjectOfType<UpgradeShop>();
        reminder.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            upgradeShop.ToggleAccess(true);
            reminder.SetActive(true);
            reminder.GetComponent<TextMeshProUGUI>().text = "'Q' Upgrade Shop";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            upgradeShop.ToggleAccess(false);
            reminder.SetActive(false);
        }
    }
}
