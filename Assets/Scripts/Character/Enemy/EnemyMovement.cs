using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float targetVariance;
    [SerializeField] float timeToRecalculateRandomTarget;
    AIPath AI;

    Vector2 currentDestination;
    Transform desiredTarget;
    Vector2 randomVector;
    NNConstraint walkableNodesOnly;
    float timer;

    private void Awake()
    {
        AI = GetComponent<AIPath>();
    }

    private void Start()
    {
        AI.maxSpeed = speed;
        RecalculateRandomTarget();
        timer = 0;
        walkableNodesOnly = new NNConstraint
        {
            constrainWalkability = true
        };
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float distanceToTarget = Vector2.Distance(desiredTarget.position, transform.position);
        if (distanceToTarget < (targetVariance + 0.5f))
        {
            AI.destination = desiredTarget.position;
        }
        else
        {
            currentDestination = GetValidDestination();
            AI.destination = currentDestination;
        }

        if (timeToRecalculateRandomTarget < timer)
        {
            RecalculateRandomTarget();
            timer = 0;
        }

        Debug.DrawLine(transform.position, (Vector2)desiredTarget.position + randomVector);

    }

    private void RecalculateRandomTarget()
    {
        randomVector = GetRandomVector();
    }

    private Vector2 GetRandomVector()
    {
        //float randomVariance = Random.Range(0, targetVariance);
        return Random.insideUnitCircle * targetVariance;
    }

    private Vector2 GetValidDestination()
    {
        Vector2 destination = (Vector2)desiredTarget.position + randomVector;
        return (Vector2)(Vector3)AstarPath.active.GetNearest(destination, walkableNodesOnly).node.position;
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
        desiredTarget = target;
    }

}
