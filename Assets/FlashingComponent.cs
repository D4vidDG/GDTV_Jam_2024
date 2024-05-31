using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlashingComponent : MonoBehaviour
{
    public bool isEnabled;
    public float flashDelay;
    public Color initialColor, flashColor;
    public TextMeshProUGUI textMeshPro;
    public Image image;
    public bool isFlashing;

    private void OnEnable()
    {
        isFlashing = false;

        StartCoroutine(Flash());
    }

    private void OnDisable()
    {
        StopCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        if (isEnabled)
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(flashDelay);

                if (isFlashing)
                {
                    if (textMeshPro != null)
                    {
                        textMeshPro.color = initialColor;
                    }
                    else if(image != null)
                    {
                        image.color = initialColor;
                    }
                    isFlashing = false;
                }
                else
                {
                    if (textMeshPro != null)
                    {
                        textMeshPro.color = flashColor;
                    }
                    else if(image != null)
                    {
                        image.color = flashColor;
                    }
                    isFlashing = true;
                }
            }
        }
    }

    public void ToggleEffect(bool toggle)
    {
        isEnabled = toggle;

        if (isEnabled)
        {
            isFlashing = false;
            StartCoroutine(Flash());
        }
        else
        {
            textMeshPro.color = initialColor;
            StopCoroutine(Flash());
        }
    }
}
