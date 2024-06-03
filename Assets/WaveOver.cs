using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveOver : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public void ToggleWaveText(bool wave)
    {
        if (wave)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
