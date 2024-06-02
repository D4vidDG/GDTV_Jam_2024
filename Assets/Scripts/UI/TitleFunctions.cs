using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleFunctions : MonoBehaviour
{
    public TitleMenu titleMenu;
    [SerializeField] int mainLevelSceneIndex;
    public Image start, options, quit;
    public Sprite startSprite, optionsSprite, quitSprite;
    public bool enableInput;

    private void Start()
    {
        enableInput = true;
    }

    public void ToggleInput(bool toggle)
    {
        enableInput = toggle;

        if (!enableInput)
        {
            start.GetComponent<Button>().enabled = false;
            options.GetComponent<Button>().enabled = false;
            quit.GetComponent<Button>().enabled = false;
        }
        else
        {
            start.GetComponent<Button>().enabled = true;
            options.GetComponent<Button>().enabled = true;
            quit.GetComponent<Button>().enabled = true;
        }
    }


    public void PressStartButton(float delay)
    {
        if (enableInput)
        {
            start.overrideSprite = startSprite;
            StartCoroutine(StartButton(delay));
        }
    }


    public IEnumerator StartButton(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1;
        SceneManager.LoadScene(mainLevelSceneIndex);//this should set to the current scene name instead
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
        titleMenu.ToggleOptions();//this should only be able to be pressed when the options button is visible
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
