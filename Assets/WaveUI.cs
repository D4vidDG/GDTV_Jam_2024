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
    public float speed;
    public bool doneFlashing;
    // Start is called before the first frame update
    void Start()
    {
        waveNumberText.text = (GameManager.instance.waveCounter + 1).ToString();
    }
    
    public void NextWave()
    {
        Debug.Log("who is calling me???");
        doneFlashing = false;
        StartCoroutine(IncreaseWaveNumber());
    }

    IEnumerator IncreaseWaveNumber()
    {
        float tick = 0f;
        for (int i = 0; i < flashCount; i++) 
        {
            tick = 0f;
            while (waveNumberText.color != flashColor)
            {
                tick += Time.deltaTime * speed;
                waveNumberText.color = Color.Lerp(startColor, flashColor, tick);
                yield return null;
            }

            tick = 0;

            while (waveNumberText.color != startColor)
            {
                tick += Time.deltaTime * speed;
                waveNumberText.color = Color.Lerp(flashColor, startColor, tick);
                yield return null;
            }
        }

        tick = 0f;
        while (waveNumberText.color != flashColor)
        {
            tick += Time.deltaTime * speed;
            waveNumberText.color = Color.Lerp(startColor, flashColor, tick);
            yield return null;
        }

        tick = 0;

        while (waveNumberText.color != startColor)
        {
            tick += Time.deltaTime * speed;
            waveNumberText.color = Color.Lerp(flashColor, startColor, tick);
            yield return null;
        }

        waveNumberText.text = (GameManager.instance.waveCounter + 1).ToString();
        doneFlashing = true;
    }
}
