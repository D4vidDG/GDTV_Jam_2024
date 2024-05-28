using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseFunctions : MonoBehaviour
{
    public Image retry, options, quit;
    public Sprite retrySprite, optionsSprite, quitSprite;
    public bool retryPressed, optionsPressed, quitPressed;
    public Color flashColor;
    public float flashDelay, flashTimer;
    public bool isFlashing, enableInput;

    private void Start()
    {
        flashTimer = 0;
        isFlashing = false;
        enableInput = true;
    }

    public void ButtonChange(int clicked)
    {
        if (enableInput) { 
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
                    if (!optionsPressed && options != null)
                    {
                        options.overrideSprite = optionsSprite;
                        optionsPressed = true;
                        enableInput = false;
                    }
                    break;
                case 2:
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
        GameOver.instance.gameObject.SetActive(true);
        Time.timeScale = 1;
        SceneManager.LoadScene("MergedScene");//this should set to the current scene name instead
    }


    public void PressOptionsButton(float delay)
    {
        if (enableInput)
        {
            StartCoroutine(OptionsButton(delay));
        }
    }


    public IEnumerator OptionsButton(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        options.overrideSprite = null;
        optionsPressed = false;
        options.color = new Color(255, 255, 255, 100);
        isFlashing = false;
        enableInput = true;
        PauseMenu.instance.ToggleOptions();//this should only be able to be pressed when the options button is visible
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


    public void Update()
    {
        if (PauseMenu.instance.isPaused)
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
                else if (optionsPressed)
                {
                    if (isFlashing)
                    {
                        options.color = flashColor;
                    }
                    else
                    {
                        options.color = new Color(255, 255, 255, 100);
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
    }
}
