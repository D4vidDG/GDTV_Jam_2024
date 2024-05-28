using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleFunctions : MonoBehaviour
{
    public Image play, options, quit;
    public Sprite playSprite, optionsSprite, quitSprite;
    public bool playPressed, optionsPressed, quitPressed;
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
        if (enableInput)
        {
            switch (clicked)
            {
                case 0:
                    if (!playPressed && play != null)
                    {
                        play.overrideSprite = playSprite;
                        playPressed = true;
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

    //might be better to merge the button press functions into 1 instead
    public void PressPlayButton(float delay)
    {
        if (enableInput)
        {
            StartCoroutine(PlayButton(delay));
        }
    }


    public IEnumerator PlayButton(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene("EnemyAIStuff");
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
        FindObjectOfType<TitleMenu>().ToggleOptions();//this should only be able to be pressed when the options button is visible
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
        flashTimer += Time.unscaledDeltaTime;

        if (flashTimer >= flashDelay)
        {
            if (playPressed)
            {
                if (isFlashing)
                {
                    play.color = flashColor;
                }
                else
                {
                    play.color = new Color(255, 255, 255, 100);
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
