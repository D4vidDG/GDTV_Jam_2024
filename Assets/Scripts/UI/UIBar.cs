
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    [SerializeField] Image bar;
    Slider slider;
    float fraction = 1f;

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
        slider.value = fraction;
    }

    public float GetFraction()
    {
        return fraction;
    }

    public void SetFraction(float fraction)
    {
        this.fraction = Mathf.Clamp(fraction, 0, 1);
    }

    public void Add(float fraction)
    {
        this.fraction += fraction;
        this.fraction = Mathf.Clamp(this.fraction, 0, 1);
    }

    public Color GetColor()
    {
        return bar.color;
    }

    public void SetColor(Color color)
    {
        bar.color = color;
    }
}