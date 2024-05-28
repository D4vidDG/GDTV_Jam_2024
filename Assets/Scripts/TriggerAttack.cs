using UnityEngine;

public class TriggerAttack : MonoBehaviour
{
    EnemyScript enemyScript;

    private void Awake()
    {
        enemyScript = GetComponentInParent<EnemyScript>();
    }
    public void Attack()
    {
        enemyScript.Attack();
    }
}
