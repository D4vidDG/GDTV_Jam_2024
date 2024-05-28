
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkColor : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float blinkFrequency;
    [SerializeField] Color targetColor;

    Color initialColor;
    private Coroutine blinkCoroutine;

    private void Start()
    {
        initialColor = image.color;
    }

    public void Trigger()
    {
        blinkCoroutine = StartCoroutine(Blink());
        initialColor = image.color;
    }

    public void Stop()
    {
        if (blinkCoroutine != null) StopCoroutine(blinkCoroutine);
        image.color = initialColor;
    }

    private IEnumerator Blink()
    {
        float t = 0;
        float maxTime = 1 / blinkFrequency;

        Color initialColor = image.color;
        Color newColor = new Color(targetColor.r, targetColor.b, targetColor.g);

        yield return new WaitUntil(() =>
        {
            Color lerpColor = Color.Lerp(initialColor, newColor, t / maxTime);
            image.color = lerpColor;

            t += Time.deltaTime;
            return t >= maxTime;
        });

        t = 0;

        yield return new WaitUntil(() =>
       {
           Color lerpColor = Color.Lerp(newColor, initialColor, t / maxTime);
           image.color = lerpColor;

           t += Time.deltaTime;
           return t >= maxTime;
       });

        Trigger();
    }
}
