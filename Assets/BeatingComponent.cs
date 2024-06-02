using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeatingComponent : MonoBehaviour
{
    public bool isEnabled;
    public float beatDelay;
    public List<Vector3> sizes;
    public bool isBeating;

    private void OnEnable()
    {
        isBeating = true;
        transform.localScale = sizes[0];
        StartCoroutine(Beat());
    }

    private void OnDisable()
    {
        StopCoroutine(Beat());
    }

    IEnumerator Beat()
    {
        int i = 0;
        if (isEnabled)
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(beatDelay);

                if (isBeating)
                {
                    if (i + 1 < sizes.Count)
                    {
                        i++;
                    }
                    else
                    {
                        i--;
                        isBeating = false;
                    }
                }
                else
                {
                    if (i - 1 >= 0)
                    {
                        i--;
                    }
                    else
                    {
                        i++;
                        isBeating = true;
                    }
                }

                transform.localScale = sizes[i];
            }
        }
    }

    public void ToggleEffect(bool toggle)
    {
        isEnabled = toggle;

        if (isEnabled)
        {
            isBeating = false;
            StartCoroutine(Beat());
        }
        else
        {
            transform.localScale = sizes[0];
            StopCoroutine(Beat());
        }
    }
}
