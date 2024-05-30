using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class ShopButton : MonoBehaviour
{
    [SerializeField] Image coinImage;
    [SerializeField] TextMeshProUGUI priceTag;
    [SerializeField] Sprite coinEnabled;
    [SerializeField] Sprite coinDisabled;
    [SerializeField] Color enabledPriceColor;
    [SerializeField] Color disabledPriceColor;

    Button button;

    public ButtonClickedEvent OnClick => button.onClick;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Enable()
    {
        button.interactable = true;
        coinImage.sprite = coinEnabled;
        priceTag.color = enabledPriceColor;
    }

    public void Disable()
    {
        button.interactable = false;
        coinImage.sprite = coinDisabled;
        priceTag.color = disabledPriceColor;
    }

    public void SetPriceTag(string value)
    {
        priceTag.text = value;
    }


}