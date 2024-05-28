using Pathfinding;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed;
    [SerializeField] float attackDistance;
    [SerializeField] float damage;
    [SerializeField] float attackCooldown;

    public float maxHP;
    public float currentHP;

    float attackCooldownTimer;


    Health health;
    Animator animator;
    Health player;
    AIPath AI;
    AIDestinationSetter AIDestinationSetter;

    private void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponentInChildren<Animator>();
        AI = GetComponent<AIPath>();
        AIDestinationSetter = GetComponent<AIDestinationSetter>();
    }


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Health>();
        AIDestinationSetter.target = player.transform;
        AI.maxSpeed = speed;
        currentHP = maxHP;
        attackCooldownTimer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        attackCooldownTimer += Time.deltaTime;
        float distanceToPlayer = GetDistanceToPlayer();
        if (distanceToPlayer < attackDistance)
        {
            AI.canMove = false;
            if (attackCooldown < attackCooldownTimer) Attack();
        }
        else
        {
            AI.canMove = true;
        }

    }


    private void LateUpdate()
    {
        animator.SetFloat("Speed", AI.canMove ? AI.maxSpeed : 0f);
        animator.SetBool("Dead", health.IsDead());
    }

    private float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    private void Attack()
    {
        player.TakeDamage(damage);
        attackCooldownTimer = 0;
    }


    public void UpdateHPBy(float mod)
    {
        currentHP += mod;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
        else if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }


    private void OnDestroy()
    {
        if (SpawnManager.instance != null) SpawnManager.instance.enemyList.Remove(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
