using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Number Settings")]
    public int maxEnemyAtOnce;
    public int enemyPerBatch;
    public int enemyPerWave;
    public int enemyLeftToSpawn;
    //for some reason i have to seperate them like this so it shows up correctly under the header

    [Header("Timer Settings")]
    public float spawnDelay;
    public float spawnTimer;

    [Header("Distance Settings")]
    public float distanceRandomness;

    [Header("Misc")]
    public bool shouldSpawn;
    public GameObject Enemy;
    public List<GameObject> enemyList = new List<GameObject>();


    // Update is called once per frame
    void Update()
    {
        if (shouldSpawn)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer > spawnDelay)
            {
                SpawnBatch();

                spawnTimer = 0;
            }
        }
    }

    //call to start a new wave
    public void NewWave()
    {
        enemyLeftToSpawn = enemyPerWave;
        spawnTimer = 0;
        shouldSpawn = true;
    }

    //call to end wave
    public void EndWave()
    {
        shouldSpawn = false;
    }

    public void SpawnBatch()
    {
        for (int i = 0; i < enemyPerBatch; i++)
        {
            if (enemyList.Count < maxEnemyAtOnce && enemyLeftToSpawn > 0)
            {
                SpawnEnemy();
            }
        }
    }

    public void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(Enemy, RandomLocation(), Quaternion.identity);

        newEnemy.GetComponent<Health>().OnDead.AddListener(() =>
        {
            RemoveEnemyFromList(newEnemy);
        });

        enemyList.Add(newEnemy);
        enemyLeftToSpawn--;
    }

    public Vector2 RandomLocation()
    {
        Camera cam = Camera.main;
        float x = 0, y = 0;

        float xyRoll = Random.Range(0, 3);//0 = use x, 1 = use y, 2 = use x and y
        float numberRoll;// rolls for positive or negative number

        if (xyRoll == 0 || xyRoll == 2)
        {
            numberRoll = Random.Range(0, 2);
            if (numberRoll == 0) 
            {
                numberRoll = -1;
            }
            x = cam.transform.position.x + cam.orthographicSize * cam.aspect * numberRoll + Random.Range(0, distanceRandomness) * numberRoll;
        }

        if (xyRoll == 1 || xyRoll == 2)
        {
            numberRoll = Random.Range(0, 2);
            if (numberRoll == 0)
            {
                numberRoll = -1;
            }
            y = cam.transform.position.y + cam.orthographicSize * numberRoll + Random.Range(0, distanceRandomness) * numberRoll;
        }


        Vector2 location = new(x, y);

        return location;
    }

    public void RemoveEnemyFromList(GameObject self)
    {
        enemyList.Remove(self);

        if (enemyList.Count <= 0)
        {
            EndWave();
            GameManager.instance.NextWave();
        }
    }
}
