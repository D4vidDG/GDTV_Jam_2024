using System.Collections;
using UnityEngine;

public class DamagedColorChangeEffect : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Color damagedColor = Color.red;
    [SerializeField] SpriteRenderer spriteRenderer;

    Coroutine damageEffectCoroutine;

    Color originalColor;

    private void Start()
    {
        originalColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        health.OnAttacked.AddListener(Trigger);
        health.OnDead.AddListener(OnDead);
    }

    private void OnDisable()
    {
        health.OnAttacked.RemoveListener(Trigger);
        health.OnDead.RemoveListener(OnDead);
    }

    private void Trigger(float damage)
    {
        if (damageEffectCoroutine == null) damageEffectCoroutine = StartCoroutine(DamageEffectCoroutine());
    }

    private void OnDead()
    {
        if (damageEffectCoroutine != null)
        {
            StopCoroutine(damageEffectCoroutine);
            damageEffectCoroutine = null;
            spriteRenderer.color = originalColor;
        }
    }

    private IEnumerator DamageEffectCoroutine()
    {
        spriteRenderer.color = damagedColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
        yield return new WaitForSeconds(0.2f);
        damageEffectCoroutine = null;
    }
}
