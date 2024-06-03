using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] int levelIndex;
    public bool enableInput;

    [Header("Images")]
    public static GameOver instance;
    public Image gameOver, background;
    public float delay1, delay2;

    [Header("Buttons")]
    public Image retry, quit;
    public Sprite retrySprite, quitSprite;

    [Header("Audio")]
    public GameObject SFX;
    public AudioSource BGM;
    public float soundVolume;

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


    private void Start()
    {
        enableInput = true;
        gameObject.SetActive(false);
    }


    public void StartGameOverScreen()
    {
        GameManager.instance.ToggleControl(false);
        BGM.volume = soundVolume;
        StartCoroutine(GameOverRoutine());
    }


    public IEnumerator GameOverRoutine()
    {
        GameObject.FindWithTag("Player").GetComponentInChildren<SpriteRenderer>().sortingLayerName = "UI";
        background.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(delay1);

        gameOver.gameObject.SetActive(true);
        SFX.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(delay2);

        retry.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
    }

    public void ToggleInput(bool toggle)
    {
        enableInput = toggle;

        if (!enableInput)
        {
            retry.GetComponent<Button>().enabled = false;
            quit.GetComponent<Button>().enabled = false;
        }
        else
        {
            retry.GetComponent<Button>().enabled = true;
            quit.GetComponent<Button>().enabled = true;
        }
    }


    public void PressRetryButton(float delay)
    {
        if (enableInput)
        {
            retry.overrideSprite = retrySprite;
            StartCoroutine(RetryButton(delay));
        }
    }

    public IEnumerator RetryButton(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        instance.gameObject.SetActive(true); //why did i make this again
        Time.timeScale = 1;
        SceneManager.LoadScene(levelIndex);//this should set to the current scene name instead
    }


    public void PressQuitButton(float delay)
    {
        if (enableInput)
        {
            quit.overrideSprite = quitSprite;
            StartCoroutine(QuitButton(delay));
        }
    }


    public IEnumerator QuitButton(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Application.Quit();
    }

    public void ButtonSound(AudioSource audio)
    {
        if (enableInput)
        {
            audio.Play();
        }
    }
}
