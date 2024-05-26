using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Core")]
    public GameObject target;
    public IAstarAI AI;
    public AIDestinationSetter AIDestinationSetter;
    public bool shouldDestroy = false;

    [Header("Stats")]
    public float maxHP;
    public float currentHP;
    public float attackDamage, attackCooldown, attackCooldownTimer;
    public bool canAttack;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        AI = gameObject.GetComponent<IAstarAI>();
        AIDestinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        currentHP = maxHP;
        attackCooldownTimer = 0;
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            AIDestinationSetter.target = target.transform;

            SearchPath();
        }

        if(shouldDestroy)
        {
            Destroy(gameObject);
        }

        if(attackCooldownTimer < attackCooldown)
        {
            attackCooldownTimer += Time.deltaTime;
        }
    }

    public void SearchPath()
    {
        AI.SearchPath();
    }

    public void UpdateHPBy(float mod)
    {
        currentHP += mod;

        if(currentHP <= 0)
        {
            Destroy(gameObject);
        }
        else if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void Attack()
    {
        if (attackCooldownTimer >= attackCooldown)
        {
            attackCooldownTimer = 0;
            Debug.Log("attacking" + attackCooldownTimer);
        }
    }

    private void OnDestroy()
    {
        SpawnManager.instance.enemyList.Remove(gameObject);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Attack();
        }
    }
}
