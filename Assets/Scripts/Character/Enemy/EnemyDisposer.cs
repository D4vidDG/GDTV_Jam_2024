using System.Collections;
using UnityEngine;

public class EnemyDiposer : MonoBehaviour
{
    [SerializeField] float corpseLifetime;
    [SerializeField] float corpseDisappearTime;

    float timer;

    Health health;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


    private void OnEnable()
    {
        health.OnDead.AddListener(DisposeBody);
    }

    private void OnDisable()
    {
        health.OnDead.RemoveListener(DisposeBody);
    }


    private void DisposeBody()
    {
        StartCoroutine(DisposeCoroutine());
    }

    private IEnumerator DisposeCoroutine()
    {
        timer = 0;
        while (timer < corpseLifetime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0;
        float alphaValue = 1;
        Color color = spriteRenderer.color;

        while (timer < corpseDisappearTime)
        {
            timer += Time.deltaTime;
            alphaValue = Mathf.Lerp(1f, 0f, timer / corpseDisappearTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, alphaValue);
            yield return null;
        }

        spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
        Destroy(this.gameObject);
    }
}
