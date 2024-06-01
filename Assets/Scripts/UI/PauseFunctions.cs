using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseFunctions : MonoBehaviour
{
    public Image retry, options, quit;
    public Sprite retrySprite, optionsSprite, quitSprite;
    public bool  enableInput;

    private void Start()
    {
        enableInput = true;
    }

    public void ToggleInput(bool toggle)
    {
        enableInput = toggle;

        if (!enableInput)
        {
            retry.GetComponent<Button>().enabled = false;
            options.GetComponent<Button>().enabled = false;
            quit.GetComponent<Button>().enabled = false;
        }
        else
        {
            retry.GetComponent<Button>().enabled = true;
            options.GetComponent<Button>().enabled = true;
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
        GameOver.instance.gameObject.SetActive(true);
        Time.timeScale = 1;
        SceneManager.LoadScene("MergedScene");//this should set to the current scene name instead
    }


    public void PressOptionsButton(float delay)
    {
        if (enableInput)
        {
            options.overrideSprite = optionsSprite;
            StartCoroutine(OptionsButton(delay));
        }
    }


    public IEnumerator OptionsButton(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        options.overrideSprite = null;
        options.color = new Color(255, 255, 255, 100);
        options.gameObject.GetComponent<FlashingComponent>().ToggleEffect(false);
        enableInput = true;
        PauseMenu.instance.ToggleOptions();//this should only be able to be pressed when the options button is visible
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
