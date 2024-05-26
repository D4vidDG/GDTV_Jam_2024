using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] UIBar healthBar;
    [SerializeField] GameObject bloodEffect;
    [SerializeField] GameObject[] disableOnDie;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    Coroutine damageEffectCoroutine;


    float health;
    bool isDead;
    bool isInvincible;

    Color originalColor;

    public Action OnDead;
    public Action<float> OnAttacked;


    private void Start()
    {
        health = maxHealth;
        originalColor = spriteRenderer.color;

    }

    private void OnEnable()
    {
        Reset();
    }

    public float GetHealthPercentage()
    {
        return (health / maxHealth) * 100;
    }
    public float GetHealth()
    {
        return health;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        if (isInvincible) return;
        health -= damage;
        if (damageEffectCoroutine == null) damageEffectCoroutine = StartCoroutine(DamageEffect());
        OnAttacked?.Invoke(damage);
        if (healthBar != null) healthBar.SetPercentage(health / maxHealth);
        if (health <= 0)
        {
            Die();
            health = 0;
            if (healthBar != null) healthBar.SetPercentage(0);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void Reset()
    {
        health = maxHealth;
        isDead = false;
        if (healthBar != null) healthBar.SetPercentage(health / maxHealth);
    }

    public void SetInvincible(bool isInvincible)
    {
        this.isInvincible = isInvincible;
    }

    private void Die()
    {
        isDead = true;
        if (animator != null) animator.SetTrigger("Die");
        if (damageEffectCoroutine != null)
        {
            StopCoroutine(damageEffectCoroutine);
            damageEffectCoroutine = null;
            spriteRenderer.color = originalColor;
        }
        if (bloodEffect != null) Instantiate(bloodEffect, transform.position, Quaternion.identity, null);
        OnDead?.Invoke();
        foreach (GameObject gameObject in disableOnDie)
        {
            gameObject.SetActive(false);
        }
    }

    public IEnumerator DamageEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
        yield return new WaitForSeconds(0.2f);
        damageEffectCoroutine = null;
    }

}
