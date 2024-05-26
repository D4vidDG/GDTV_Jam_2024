using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
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
    public float minDistance;
    public float maxDistance;

    [Header("Misc")]
    public bool shouldSpawn;
    public GameObject Enemy;
    public List<GameObject> enemyList = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
    }


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
        GameObject newEnemy = Instantiate(Enemy, randomLocation(), Quaternion.identity);
        enemyList.Add(newEnemy);
        enemyLeftToSpawn--;
    }

    public Vector2 randomLocation()
    {
        float x, y;

        float roll = Random.Range(0, 2);
        if(roll == 0) // rolls for positive or negative number
        {
            roll = -1;
        }
        x = Random.Range(minDistance, maxDistance) * roll;

        roll = Random.Range(0, 2);
        if (roll == 0)
        {
            roll = -1;
        }
        y = Random.Range(minDistance, maxDistance) * roll;


        Vector2 location = new Vector2(x, y);

        return location;
    }
}
