using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    public float gameOverSpeed, backgroundSpeed;
    public Image gameOver, background;
    public Color gameOverStartColor, gameOverEndColor;
    public Color backgroundStartColor, backgroundEndColor;

    public Image retry, quit;
    public Sprite retrySprite, quitSprite;
    public bool retryPressed, quitPressed;
    public Color flashColor;
    public float flashDelay, flashTimer;
    public bool isFlashing, enableInput;


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
        flashTimer = 0;
        isFlashing = false;
        enableInput = true;
        gameObject.SetActive(false);
    }


    public void Update()
    {
        flashTimer += Time.unscaledDeltaTime;

        if (flashTimer >= flashDelay)
        {
            if (retryPressed)
            {
                if (isFlashing)
                {
                    retry.color = flashColor;
                }
                else
                {
                    retry.color = new Color(255, 255, 255, 100);
                }
            }
            else if (quitPressed)
            {
                if (isFlashing)
                {
                    quit.color = flashColor;
                }
                else
                {
                    quit.color = new Color(255, 255, 255, 100);
                }
            }

            isFlashing = !isFlashing;

            flashTimer = 0;
        }
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
        float tick = 0;

        while (gameOver.color != gameOverEndColor)
        {
            tick += Time.deltaTime * gameOverSpeed;
            gameOver.color = Color.Lerp(gameOver.color, gameOverEndColor, tick);
            yield return null;
        }

        tick = 0;

        while (background.color != backgroundEndColor)
        {
            tick += Time.deltaTime * backgroundSpeed;
            background.color = Color.Lerp(background.color, backgroundEndColor, tick);
            yield return null;
        }

        retry.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
    }

    public void ButtonChange(int clicked)
    {
        if (enableInput)
        {
            switch (clicked)
            {
                case 0:
                    if (!retryPressed && retry != null)
                    {
                        retry.overrideSprite = retrySprite;
                        retryPressed = true;
                        enableInput = false;
                    }
                    break;
                case 1:
                    if (!quitPressed && quit != null)
                    {
                        quit.overrideSprite = quitSprite;
                        quitPressed = true;
                        enableInput = false;
                    }
                    break;
            }
        }
    }


    public void PressRetryButton(float delay)
    {
        if (enableInput)
        {
            StartCoroutine(RetryButton(delay));
        }
    }

    public IEnumerator RetryButton(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        instance.gameObject.SetActive(true);
        Time.timeScale = 1;
        SceneManager.LoadScene("MergedScene");//this should set to the current scene name instead
    }


    public void PressQuitButton(float delay)
    {
        if (enableInput)
        {
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
