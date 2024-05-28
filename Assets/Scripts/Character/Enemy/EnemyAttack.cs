using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float attackDistance;
    [SerializeField] float damage;
    [SerializeField] float attackCooldown;

    Health player;
    Animator animator;
    AnimationEventTrigger attackAnimationTrigger;


    float attackCooldownTimer;
    bool attacking;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Health>();
        animator = GetComponentInChildren<Animator>();
        attackAnimationTrigger = animator.GetComponent<AnimationEventTrigger>();
    }

    private void OnEnable()
    {
        attackAnimationTrigger.OnEventTriggered += AttackOnAnimation;
    }

    private void OnDisable()
    {
        attackAnimationTrigger.OnEventTriggered -= AttackOnAnimation;
    }

    void Start()
    {
        attackCooldownTimer = 0;
        attacking = false;
    }

    private void Update()
    {
        attackCooldownTimer += Time.deltaTime;
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        attacking = true;
    }

    private void AttackOnAnimation()
    {
        player.TakeDamage(damage);
        attackCooldownTimer = 0;
        attacking = false;
    }

    public bool CanAttack()
    {
        return IsPlayerWithinAttackRange() && attackCooldown < attackCooldownTimer && !attacking;
    }

    public bool IsAttacking()
    {
        return attacking;
    }

    public bool IsPlayerWithinAttackRange()
    {
        float distanceToPlayer = GetDistanceToPlayer();
        return distanceToPlayer < attackDistance;
    }

    private float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

}