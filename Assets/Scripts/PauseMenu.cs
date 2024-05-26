using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public GameObject PauseScreen;
    public bool IsPaused;
    // Start is called before the first frame update
    void Start()
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

        IsPaused = false;
        PauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (IsPaused)
        {
            Time.timeScale = 1.0f;
            PauseScreen.SetActive(false);
            IsPaused = false;
        }
        else
        {
            Time.timeScale = 0.0f;
            PauseScreen.SetActive(true);
            IsPaused = true;
        }
    }
}
