using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public int waveCounter;

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

    public void StartWave()
    {
        SpawnManager.instance.NewWave();
    }

    public void OpenUpgradeMenu()
    {
        Debug.Log("menu here");
    }

    public void IncreaseDifficulty()
    {
        SpawnManager sm = SpawnManager.instance;

        sm.maxEnemyAtOnce += 1 * waveCounter;
        sm.enemyPerBatch += 1 * waveCounter;
        sm.enemyPerWave += 1 * waveCounter;
    }
}
