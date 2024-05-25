using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public int maxEnemyAtOnce, enemyPerBatch, enemyPerWave;
    public float spawnDelay, spawnTimer;
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

        spawnTimer = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        SpawnBatch();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer > spawnDelay)
        {
            SpawnBatch();

            spawnTimer = 0;
        }
    }

    public void SpawnBatch()
    {
        for (int i = 0; i < enemyPerBatch; i++)
        {
            if (enemyList.Count < maxEnemyAtOnce)
            {
                SpawnEnemy();
            }
        }
    }

    public void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(Enemy, randomLocation(), Quaternion.identity);
        enemyList.Add(newEnemy);
    }

    public Vector2 randomLocation()
    {
        float x, y;

        float roll = Random.Range(0, 1);
        if(roll == 0) // rolls for positive or negative number
        {
            roll = -1;
        }
        x = Random.Range(10, 20) * roll;

        roll = Random.Range(0, 1);
        if (roll == 0)
        {
            roll = -1;
        }
        y = Random.Range(10, 20) * roll;


        Vector2 location = new Vector2(x, y);

        return location;
    }
}
