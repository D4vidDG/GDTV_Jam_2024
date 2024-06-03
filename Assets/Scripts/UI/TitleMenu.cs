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
        if (Input.GetKeyDown(KeyCode.Escape) && isOnOptions)
        {
            ToggleOptions();
        }
    }

    public void ToggleOptions()
    {
        if (isOnOptions)
        {
            menuScreen.SetActive(true);
            optionsMenu.SetActive(false);
            isOnOptions = false;
            transform.GetComponentInChildren<TitleFunctions>(true).ToggleInput(true);
        }
        else
        {
            menuScreen.SetActive(false);
            optionsMenu.SetActive(true);
            isOnOptions = true;
            transform.GetComponentInChildren<TitleFunctions>(true).ToggleInput(false);
        }
    }
}
