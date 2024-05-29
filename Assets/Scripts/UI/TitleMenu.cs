using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    public GameObject menuScreen, optionsMenu;
    public bool disablePauseMenu, isPaused, isOnOptions;
    // Start is called before the first frame update
    void Start()
    {
        if (disablePauseMenu)
        {
            menuScreen.SetActive(false);
            isPaused = false;
        }
        else
        {
            menuScreen.SetActive(true);
            isPaused = true;
        }
        optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOnOptions)
            {
                TogglePause();
            }
            else
            {
                ToggleOptions();
            }
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1.0f;
            menuScreen.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0.0f;
            menuScreen.SetActive(true);
            isPaused = true;
        }
    }

    public void ToggleOptions()
    {
        if (isOnOptions)
        {
            menuScreen.SetActive(true);
            optionsMenu.SetActive(false);
            isOnOptions = false;
        }
        else
        {
            menuScreen.SetActive(false);
            optionsMenu.SetActive(true);
            isOnOptions = true;
        }
    }
}
