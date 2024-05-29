using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyScript : MonoBehaviour
{
    public List<AudioClip> noiseClips;
    public float soundDelay, soundTimer;
    public float minDelay, maxDelay;


    Health health;
    Animator animator;
    EnemyMovement movement;
    EnemyAttack attacker;
    Collider2D[] colliders;
    CharacterFacer facer;


    private void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<EnemyMovement>();
        attacker = GetComponent<EnemyAttack>();
        colliders = GetComponentsInChildren<Collider2D>();
        facer = GetComponent<CharacterFacer>();
    }


    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        facer.SetTarget(player.transform);
        facer.Enable();
        movement.SetTarget(player.transform);
        RandomDelay();
        soundTimer = 0;
    }

    private void OnEnable()
    {
        health.OnDead.AddListener(OnDead);
    }

    private void OnDisable()
    {
        health.OnDead.RemoveListener(OnDead);
    }

    void Update()
    {
        if (health.IsDead()) return;


        if (attacker.IsPlayerWithinAttackRange() || attacker.IsAttacking())
        {
            movement.Stop();
        }
        else
        {
            movement.Move();
        }

        if (attacker.CanAttack())
        {
            attacker.Attack();
        }

        soundTimer += Time.deltaTime;

        if (soundTimer >= soundDelay)
        {
            RandomDelay();
            int i = Random.Range(0, noiseClips.Count);
            AudioSource.PlayClipAtPoint(noiseClips[i], transform.position);
            soundTimer = 0;
        }
    }

    public void RandomDelay()
    {
        soundDelay = Random.Range(minDelay, maxDelay);
    }


    private void LateUpdate()
    {
        animator.SetBool("Moving", movement.IsMoving());
        animator.SetBool("Dead", health.IsDead());
    }

    private void OnDead()
    {
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        movement.Stop();
        facer.Disable();
    }

    // private void OnDestroy()
    // {
    //     if (SpawnManager.instance != null) SpawnManager.instance.enemyList.Remove(gameObject);
    // }

    // public void UpdateHPBy(float mod)
    // {
    //     currentHP += mod;

    //     if (currentHP <= 0)
    //     {
    //         Destroy(gameObject);
    //     }
    //     else if (currentHP > maxHP)
    //     {
    //         currentHP = maxHP;
    //     }
    // }
}
