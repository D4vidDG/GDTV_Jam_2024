using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float timeBetweenWaves;
    public UpgradeShop upgradeShop;
    public WeaponShop weaponShop;
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
        if (weaponShop == null)
        {
            weaponShop = FindObjectOfType<WeaponShop>();
        }
        if(upgradeShop == null)
        {
            upgradeShop = FindObjectOfType<UpgradeShop>();
        }
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
        //upgradeShop.ToggleAccess(false);
        //weaponShop.ToggleAccess(false);
        IncreaseDifficulty();
        FindObjectOfType<SpawnManager>().NewWave();
    }

    public void EndWave()
    {
        //upgradeShop.ToggleAccess(true);
        //weaponShop.ToggleAccess(true);
    }

    public void NextWave()
    {
        waveCounter++;
        StartCoroutine(NextWaveWait());
    }

    IEnumerator NextWaveWait()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
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
            if (FindObjectOfType<PauseFunctions>() != null)
            {
                FindObjectOfType<PauseFunctions>().ToggleInput(false);
            }
            if(FindObjectOfType<WaveUI>() != null)
            {
                FindObjectOfType<WaveUI>().transform.parent.gameObject.SetActive(false);//very roundabout way for disabling the HUD
            }
            if (FindObjectOfType<MouseLookAheadTarget>() != null)
            {
                FindObjectOfType<MouseLookAheadTarget>().transform.parent.gameObject.SetActive(false);
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
    }

    public void ToggleControl(bool toggle)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.Enable(toggle);
    }
}
