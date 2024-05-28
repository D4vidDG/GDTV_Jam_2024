using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public int waveCounter;
    public bool playerDead;

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
        playerDead = false;
        waveCounter = 0;
    }

    private void Start()
    {
        Startup();
        StartWave();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MergedScene")
        {
            Startup();
            StartWave();
        }
    }

    public void StartWave()
    {
        IncreaseDifficulty();
        FindObjectOfType<SpawnManager>().NewWave();
    }

    public void NextWave()
    {
        waveCounter++;
        FindObjectOfType<WaveUI>().NextWave();
        StartCoroutine(NextWaveWait());
    }

    IEnumerator NextWaveWait()
    {
        WaveUI wui = FindObjectOfType<WaveUI>();
        while (!wui.doneFlashing) 
        {
            yield return null;
        }
        StartWave();
    }


    public void OpenUpgradeMenu()
    {
        Debug.Log("menu here");
    }

    public void IncreaseDifficulty()
    {
        SpawnManager sm = FindObjectOfType<SpawnManager>();

        sm.maxEnemyAtOnce += 1 * waveCounter;
        sm.enemyPerBatch += 1 * waveCounter;
        sm.enemyPerWave += 1 * waveCounter;
    }

    public void PlayerKilled()
    {
        if (!playerDead)
        {
            playerDead = true;
            if(FindObjectOfType<PauseFunctions>() != null)
            {
                FindObjectOfType<PauseFunctions>().enableInput = false;
            }
            GameOver.instance.gameObject.SetActive(true);
            GameOver.instance.StartGameOverScreen();
        }
    }

    public void Startup()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>().gameObject;
        }
        waveCounter = 0;
        playerDead = false;
    }
}
