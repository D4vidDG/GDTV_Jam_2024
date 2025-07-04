
using System.Collections;
using UnityEngine;

public class BlinkAlpha : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float blinkFrequency;
    [SerializeField][Range(0f, 1f)] float targetAlpha = 0f;

    Color initialColor;
    private Coroutine blinkCoroutine;

    private void Start()
    {
        initialColor = spriteRenderer.color;
    }


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
        Color newColor = new Color(initialColor.r, initialColor.b, initialColor.g, targetAlpha);

        yield return new WaitUntil(() =>
        {
            Color lerpColor = Color.Lerp(initialColor, newColor, t / maxTime);
            spriteRenderer.color = lerpColor;

            t += Time.deltaTime;
            return t >= maxTime;
        });

        t = 0;

        yield return new WaitUntil(() =>
       {
           Color lerpColor = Color.Lerp(newColor, initialColor, t / maxTime);
           spriteRenderer.color = lerpColor;

           t += Time.deltaTime;
           return t >= maxTime;
       });

        Trigger();
    }
}
