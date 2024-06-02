using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float timeBetweenWaves;
    public GameObject player;
    public WaveOver waveOver;
    public int waveCounter;
    public bool playerDead;

    bool inBetweenWaves = false;

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
        if (scene.buildIndex == 1)
        {
            Startup();
            StartWave();
        }
    }

    private void Update()
    {
        if (inBetweenWaves && Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            inBetweenWaves = false;
            StartCoroutine(NewWaveIntro());
        }
    }

    public void StartWave()
    {
        //upgradeShop.ToggleAccess(false);
        //weaponShop.ToggleAccess(false);
        IncreaseDifficulty();
        FindObjectOfType<SpawnManager>().NewWave();
        waveOver.ToggleWaveText(false);
    }

    public void EndWave()
    {
        //upgradeShop.ToggleAccess(true);
        //weaponShop.ToggleAccess(true);
        waveOver.ToggleWaveText(true);
    }

    public void NextWave()
    {
        waveCounter++;
        StartCoroutine(NextWaveWait());
    }

    IEnumerator NextWaveWait()
    {
        inBetweenWaves = true;
        yield return new WaitForSeconds(timeBetweenWaves);
        inBetweenWaves = false;
        StartCoroutine(NewWaveIntro());
    }

    private IEnumerator NewWaveIntro()
    {
        waveOver.ToggleWaveText(false);
        WaveUI wui = FindObjectOfType<WaveUI>();
        wui.NextWave();

        while (!wui.doneFlashing)
        {
            yield return null;
        }
        StartWave();
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
            //this whole part is a very roundabout way for disabling the HUD
            if (FindObjectOfType<PauseFunctions>() != null)
            {
                FindObjectOfType<PauseFunctions>().ToggleInput(false);
            }
            if (FindObjectOfType<WaveUI>() != null)
            {
                FindObjectOfType<WaveUI>().transform.parent.gameObject.SetActive(false);
            }
            if (FindObjectOfType<MouseLookAheadTarget>() != null)
            {
                FindObjectOfType<MouseLookAheadTarget>().transform.parent.gameObject.SetActive(false);
            }
            if (FindObjectOfType<QuestPointer>() != null)
            {
                FindObjectOfType<QuestPointer>().transform.parent.gameObject.SetActive(false);
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

        player.GetComponent<Health>().OnDead.AddListener(PlayerKilled);
        waveCounter = 0;
        playerDead = false;

        waveOver = FindObjectOfType<WaveOver>();
    }

    public void ToggleControl(bool toggle)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.Enable(toggle);
    }
}
