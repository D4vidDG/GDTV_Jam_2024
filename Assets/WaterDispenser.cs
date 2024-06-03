using TMPro;
using UnityEngine;

public class WaterDispenser : MonoBehaviour
{
    [SerializeField] int price;
    [SerializeField] float healingPercentage;
    [SerializeField] TextMeshProUGUI interactionTextMesh;

    CoinWallet coinWallet;
    Health playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        coinWallet = FindObjectOfType<CoinWallet>();
    }

    private void Start()
    {
        interactionTextMesh.text = "Press E\n" + healingPercentage + "% <sprite=0>for " + price + "<sprite=1>";
    }

    public void SellHealing()
    {
        coinWallet.Spend(price);
        float healingAmount = healingPercentage * playerHealth.GetMaxHealth() / 100;
        playerHealth.Heal(healingAmount);
        AudioManager.instance.PlaySound(SoundName.Heal);
    }
}
