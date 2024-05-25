using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILogic : MonoBehaviour
{
    public GameObject target;
    public IAstarAI AI;
    public AIDestinationSetter AIDestinationSetter;
    public bool shouldDestroy = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        AI = gameObject.GetComponent<IAstarAI>();
        AIDestinationSetter = gameObject.GetComponent<AIDestinationSetter>();
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
    }

    public void SearchPath()
    {
        AI.SearchPath();
    }

    public void OnDeath()
    {

    }

    private void OnDestroy()
    {
        SpawnManager.instance.enemyList.Remove(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
    }
}
