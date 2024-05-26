using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseFunctions : MonoBehaviour
{
    public static PauseFunctions instance;
    public Image retry, options, quit;
    public Sprite retrySprite, optionsSprite, quitSprite;
    public bool retryPressed, optionsPressed, quitPressed;
    public Color flashColor;
    public float flashDelay, flashTimer;
    public bool isFlashing;

    private void Start()
    {
        flashTimer = 0;
    }

    public void ButtonChange(int clicked)
    {
        switch (clicked)
        {
            case 0:
                if (!retryPressed)
                {
                    retry.overrideSprite = retrySprite;
                    retryPressed = true;
                }
                else
                {
                    retry.overrideSprite = null;
                    retryPressed = false;
                }
                break;
            case 1:
                if (!optionsPressed)
                {
                    options.overrideSprite = optionsSprite;
                    optionsPressed = true;
                }
                else
                {
                    options.overrideSprite = null;
                    optionsPressed = false;
                }
                break;
            case 2:
                if (!quitPressed)
                {
                    quit.overrideSprite = quitSprite;
                    quitPressed = true;
                }
                else
                {
                    quit.overrideSprite = null;
                    quitPressed = false;
                }
                break;
        }
    }

    public void Update()
    {
        if (PauseMenu.instance.IsPaused)
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
