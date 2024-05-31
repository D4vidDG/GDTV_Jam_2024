using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashingComponent : MonoBehaviour
{
    public bool isEnabled;
    public float flashDelay;
    public Color initialColor, flashColor;
    public TextMeshProUGUI textMeshPro;
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
                yield return new WaitForSeconds(flashDelay);

                if (isFlashing)
                {
                    textMeshPro.color = initialColor;
                    isFlashing = false;
                }
                else
                {
                    textMeshPro.color = flashColor;
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
