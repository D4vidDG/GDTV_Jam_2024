using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    AIPath AI;
    AIDestinationSetter destinationSetter;

    private void Awake()
    {
        AI = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Start()
    {
        AI.maxSpeed = speed;
    }

    public void Move()
    {
        AI.canMove = true;
    }

    public void Stop()
    {
        AI.canMove = false;
    }

    public bool IsMoving()
    {
        return AI.canMove;
    }

    public void SetTarget(Transform target)
    {
        destinationSetter.target = target;
    }

}
