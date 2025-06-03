using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class SaleButton : MonoBehaviour
{
    [SerializeField] Image coinImage;
    [SerializeField] TextMeshProUGUI priceTag;
    [SerializeField] Sprite coinEnabledSprite;
    [SerializeField] Sprite coinDisabledSprite;
    [SerializeField] Color priceTextEnabledColor;
    [SerializeField] Color priceTextDisabledColor;

    Button button;

    public ButtonClickedEvent OnClick => button.onClick;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Enable()
    {
        button.interactable = true;
        coinImage.sprite = coinEnabledSprite;
        priceTag.color = priceTextEnabledColor;
    }

    public void Disable()
    {
        button.interactable = false;
        coinImage.sprite = coinDisabledSprite;
        priceTag.color = priceTextDisabledColor;
    }

    public void SetPriceTag(string value)
    {
        priceTag.text = value;
    }
}