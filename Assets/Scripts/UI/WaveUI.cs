using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public TextMeshProUGUI waveNumberText;
    public Color startColor, flashColor;
    public int flashCount;
    public float flashDelay;
    public bool isFlashing, doneFlashing;
    // Start is called before the first frame update
    void Start()
    {
        waveNumberText.text = (GameManager.instance.waveCounter + 1).ToString();
    }
    
    public void NextWave()
    {
        doneFlashing = false;
        isFlashing = false;
        StartCoroutine(IncreaseWaveNumber());
    }

    IEnumerator IncreaseWaveNumber()
    {
        for (int i = 0; i < flashCount; i++)
        {
            yield return new WaitForSeconds(flashDelay);

            if (isFlashing)
            {
                waveNumberText.color = startColor;
                isFlashing = false;
            }
            else
            {
                waveNumberText.color = flashColor;
                isFlashing = true;
            }
        }

        if (isFlashing)
        {
            waveNumberText.color = startColor;
        }

        waveNumberText.text = (GameManager.instance.waveCounter + 1).ToString();
        doneFlashing = true;
    }
}
