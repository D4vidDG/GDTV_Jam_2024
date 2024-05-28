using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float corpseLifetime;
    [Header("Stats")]

    float corpseTimer;


    Health health;
    Animator animator;
    EnemyMovement movement;
    EnemyAttack attacker;
    Collider2D myCollider;
    CharacterFacer facer;


    private void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<EnemyMovement>();
        attacker = GetComponent<EnemyAttack>();
        myCollider = GetComponent<Collider2D>();
        facer = GetComponent<CharacterFacer>();
    }


    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        facer.SetTarget(player.transform);
        facer.Enable();
        movement.SetTarget(player.transform);
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
    }


    private void LateUpdate()
    {
        animator.SetBool("Moving", movement.IsMoving());
        animator.SetBool("Dead", health.IsDead());
    }

    private void OnDead()
    {
        myCollider.enabled = false;
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
