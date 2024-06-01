using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveOver : MonoBehaviour
{
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
