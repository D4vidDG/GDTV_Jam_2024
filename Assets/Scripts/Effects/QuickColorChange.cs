using System.Collections;
using UnityEngine;

public class QuickColorChange : MonoBehaviour
{
    [SerializeField] Color color = Color.red;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float effectDuration;
    [SerializeField] float timeWindowBetweenChanges;

    Coroutine effectCoroutine;

    Color originalColor;

    private void Start()
    {
        originalColor = spriteRenderer.color;
    }

    public void Trigger(float damage)
    {
        if (effectCoroutine == null) effectCoroutine = StartCoroutine(EffectCoroutine());
    }

    public void Stop()
    {
        if (effectCoroutine != null)
        {
            StopCoroutine(effectCoroutine);
            effectCoroutine = null;
            spriteRenderer.color = originalColor;
        }
    }

    private IEnumerator EffectCoroutine()
    {
        float halfDuration = effectDuration / 2;
        spriteRenderer.color = color;
        yield return new WaitForSeconds(effectDuration);
        spriteRenderer.color = originalColor;
        yield return new WaitForSeconds(timeWindowBetweenChanges);
        effectCoroutine = null;
    }
}
