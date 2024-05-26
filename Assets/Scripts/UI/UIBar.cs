
using System;
using UnityEngine;
using UnityEngine.UI;


public class UIBar : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] Image cost;
    Slider slider;
    float percentage = 1f;

    RectTransform rectTransform;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        slider.value = 1;
    }

    private void Update()
    {
        slider.value = percentage;
        if (cost != null) UpdateCostBarPosition();
    }

    public float GetPercentage()
    {
        return percentage;
    }

    public void SetPercentage(float percentage)
    {
        this.percentage = Mathf.Clamp(percentage, 0, 1);
    }

    public void Add(float percentage)
    {
        this.percentage += percentage;
        this.percentage = Mathf.Clamp(this.percentage, 0, 1);
    }

    public Color GetColor()
    {
        return bar.color;
    }

    public void SetColor(Color color)
    {
        bar.color = color;
    }

    private void UpdateCostBarPosition()
    {
        cost.rectTransform.anchoredPosition = new Vector2(percentage * rectTransform.rect.width, cost.rectTransform.anchoredPosition.y);
    }

    public void EnableCost(bool enable)
    {
        cost.enabled = enable;
    }

    internal string Blink()
    {
        throw new NotImplementedException();
    }

    public void SetCost(float value)
    {
        cost.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value * rectTransform.rect.width);
    }
}
