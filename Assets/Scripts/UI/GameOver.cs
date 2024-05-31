using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    public float gameOverFadeInTime, backgroundFadeInTime;
    public Image gameOver, background;
    public Color gameOverStartColor, gameOverEndColor;
    public Color backgroundStartColor, backgroundEndColor;

    public Image retry, quit;
    public Sprite retrySprite, quitSprite;
    public bool enableInput;


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
        gameOver.gameObject.SetActive(true);
        gameOver.color = gameOverStartColor;
        background.gameObject.SetActive(true);
        background.color = backgroundStartColor;
        StartCoroutine(GameOverRoutine());
    }


    public IEnumerator GameOverRoutine()
    {
        float timer = 0;

        while (gameOver.color != gameOverEndColor)
        {
            timer += Time.deltaTime;
            gameOver.color = Color.Lerp(gameOver.color, gameOverEndColor, timer / gameOverFadeInTime);
            yield return null;
        }

        timer = 0;

        while (background.color != backgroundEndColor)
        {
            timer += Time.deltaTime;
            background.color = Color.Lerp(background.color, backgroundEndColor, timer / backgroundFadeInTime);
            yield return null;
        }

        retry.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
    }

    public void ToggleInput(bool toggle)
    {
        enableInput = toggle;
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
        SceneManager.LoadScene("MergedScene");//this should set to the current scene name instead
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
        audio.Play();
    }
}
