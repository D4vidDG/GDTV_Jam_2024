using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;

    public List<AudioClip> hitSounds, deathSound;

    float health;
    bool isDead;
    bool isInvincible;

    public UnityEvent<float> OnAttacked;
    public UnityEvent OnDead;


    private void Start()
    {
        health = maxHealth;
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
        health = Mathf.Max(0, health - damage);
        OnAttacked?.Invoke(damage);

        if (health == 0)
        {
            AudioSource.PlayClipAtPoint(deathSound[Random.Range(0, deathSound.Count)], transform.position);
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(hitSounds[Random.Range(0, hitSounds.Count)], transform.position);
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
    }

    public void SetInvincible(bool isInvincible)
    {
        this.isInvincible = isInvincible;
    }

    private void Die()
    {
        isDead = true;
        OnDead?.Invoke();
    }

    private void OnDestroy()
    {
        OnAttacked.RemoveAllListeners();
        OnDead.RemoveAllListeners();
    }
}
