using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    const int MAX_ITERATIONS = 100;
    [SerializeField] LayerMask obstaclesLayer;
    [SerializeField] float radiusForCollisionCheck;
    [Header("Number Settings")]
    public int maxEnemyAtOnce;
    public int enemyPerBatch;
    public int enemyPerWave;
    int enemyLeftToSpawn;
    //for some reason i have to seperate them like this so it shows up correctly under the header

    [Header("Timer Settings")]
    public float spawnDelay;
    float spawnTimer;

    [Header("Distance Settings")]
    public float distanceRandomness;

    [Header("Misc")]
    public bool shouldSpawn;
    public GameObject Enemy;
    public List<GameObject> enemyList = new List<GameObject>();

    Camera mainCamera;
    int iterations; //make sure code doesn't iterate infinitely looking for a location
    ContactFilter2D filterForLocation;

    private void Start()
    {
        mainCamera = Camera.main;
        iterations = 0;
        filterForLocation = new ContactFilter2D();
        filterForLocation.ClearDepth();
        filterForLocation.SetLayerMask(obstaclesLayer);
        filterForLocation.useLayerMask = true;
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
        GameManager.instance.EndWave();
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
        GameObject newEnemy = Instantiate(Enemy, Vector2.zero, Quaternion.identity);
        Vector2 randomLocation = RandomLocation();

        iterations = 0;
        while (!IsLocationValid(randomLocation, newEnemy))
        {
            iterations++;
            if (iterations > MAX_ITERATIONS)
            {
                Debug.LogWarning("Too many iterations while looking for spawning location");
                return;
            }
            randomLocation = RandomLocation();
        }

        newEnemy.transform.position = randomLocation;
        newEnemy.GetComponent<Health>().OnDead.AddListener(() =>
        {
            RemoveEnemyFromList(newEnemy);
        });

        enemyList.Add(newEnemy);
        enemyLeftToSpawn--;
    }

    public Vector2 RandomLocation()
    {
        Vector2 xDirection = Random.value > 0.5f ? Vector2.right : Vector2.left;
        Vector2 yDirection = Random.value > 0.5f ? Vector2.up : Vector2.down;

        float xDelta = (mainCamera.orthographicSize * mainCamera.aspect)
            + Random.Range(0, distanceRandomness);
        float yDelta = mainCamera.orthographicSize
            + Random.Range(0, distanceRandomness);

        Vector2 location = (Vector2)mainCamera.transform.position
            + (xDirection * xDelta) + (yDirection * yDelta);

        Debug.DrawLine((Vector2)mainCamera.transform.position, location, Color.blue, 2f);
        NNInfo nodeInfo = AstarPath.active.GetNearest(location);
        return (Vector2)(Vector3)nodeInfo.node.position;

    }

    private bool IsLocationValid(Vector2 location, GameObject enemyInstance)
    {
        NNInfo nodeInfo = AstarPath.active.GetNearest(location);
        if (!nodeInfo.node.Walkable) return false;
        Collider2D[] results = new Collider2D[2];
        Collider2D hit = Physics2D.OverlapCircle(location, radiusForCollisionCheck, obstaclesLayer);
        if (hit != null)
        {
            Debug.Log("failed collider test");

            return false;
        }

        Debug.Log(" not failed collider test");


        return true;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Camera mainCam = Camera.main;
        Vector2 cameraPos = (Vector2)Camera.main.transform.position;
        float xDistance = (mainCam.orthographicSize * mainCam.aspect) + distanceRandomness;
        float yDistance = mainCam.orthographicSize + distanceRandomness;
        Gizmos.DrawLine(cameraPos, cameraPos + Vector2.right * xDistance);
        Gizmos.DrawLine(cameraPos, cameraPos + Vector2.left * xDistance);
        Gizmos.DrawLine(cameraPos, cameraPos + Vector2.up * yDistance);
        Gizmos.DrawLine(cameraPos, cameraPos + Vector2.down * yDistance);
    }
}
