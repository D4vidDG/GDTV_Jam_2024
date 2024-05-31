using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavingComponent : MonoBehaviour
{
    public bool isEnabled;
    public float waveDelay;
    public Vector3 initialPosition, wavePositionUp, wavePositionDown;
    public TextMeshProUGUI textMeshPro;
    public bool isGoingUp;
    public int positionIndex;
    RectTransform rectTransform;

    private void OnEnable()
    {
        isGoingUp = false;
        positionIndex = 1;
        rectTransform = textMeshPro.rectTransform;
        StartCoroutine(Wave());
    }

    private void OnDisable()
    {
        StopCoroutine(Wave());
    }

    IEnumerator Wave()
    {
        if (isEnabled)
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(waveDelay);

                if (isGoingUp)
                {
                    switch (positionIndex)
                    {
                        case 0:
                            rectTransform.anchoredPosition = initialPosition;
                            positionIndex = 1;
                            break;
                        case 1:
                            rectTransform.anchoredPosition = wavePositionUp;
                            positionIndex = 2;
                            break;
                        case 2:
                            rectTransform.anchoredPosition = initialPosition;
                            positionIndex = 1;
                            isGoingUp = false;
                            break;
                    }
                }
                else
                {
                    switch (positionIndex)
                    {
                        case 0:
                            rectTransform.anchoredPosition = initialPosition;
                            positionIndex = 1;
                            isGoingUp = true;
                            break;
                        case 1:
                            rectTransform.anchoredPosition = wavePositionDown;
                            positionIndex = 0;
                            break;
                        case 2:
                            rectTransform.anchoredPosition = initialPosition;
                            positionIndex = 1;
                            break;
                    }
                }
            }
        }
    }

    public void ToggleEffect(bool toggle)
    {
        isEnabled = toggle;

        if (isEnabled)
        {
            isGoingUp = false;
            positionIndex = 1;
            rectTransform = textMeshPro.rectTransform;
            StartCoroutine(Wave());
        }
        else
        {
            rectTransform.position = initialPosition;
            StopCoroutine(Wave());
        }
    }
}
