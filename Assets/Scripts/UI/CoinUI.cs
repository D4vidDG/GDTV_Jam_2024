using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    CoinWallet coinWallet;

    private void Awake()
    {
        coinWallet = FindObjectOfType<CoinWallet>();
    }

    private void Update()
    {
        coinText.text = "x" + coinWallet.GetCoinsCollected();
    }
}
