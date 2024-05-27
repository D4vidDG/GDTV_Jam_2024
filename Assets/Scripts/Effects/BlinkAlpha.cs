
using System.Collections;
using UnityEngine;

public class BlinkAlpha : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float blinkFrequency;

    Color initialColor;
    private Coroutine blinkCoroutine;

    public void Trigger()
    {
        blinkCoroutine = StartCoroutine(Blink());
        initialColor = spriteRenderer.color;
    }

    public void Stop()
    {
        if (blinkCoroutine != null) StopCoroutine(blinkCoroutine);
        spriteRenderer.color = initialColor;
    }

    private IEnumerator Blink()
    {
        float t = 0;
        float maxTime = 1 / blinkFrequency;

        Color initialColor = spriteRenderer.color;
        Color fadedColor = new Color(initialColor.r, initialColor.b, initialColor.g, 0f);

        yield return new WaitUntil(() =>
        {
            Color lerpColor = Color.Lerp(initialColor, fadedColor, t / maxTime);
            spriteRenderer.color = lerpColor;

            t += Time.deltaTime;
            return t >= maxTime;
        });

        t = 0;

        yield return new WaitUntil(() =>
       {
           Color lerpColor = Color.Lerp(fadedColor, initialColor, t / maxTime);
           spriteRenderer.color = lerpColor;

           t += Time.deltaTime;
           return t >= maxTime;
       });

        Trigger();
    }
}
