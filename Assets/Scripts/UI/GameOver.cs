using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    public float gameOverSpeed, backgroundSpeed;
    public Image gameOver, background;
    public Color gameOverStartColor, gameOverEndColor;
    public Color backgroundStartColor, backgroundEndColor;

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
        gameObject.SetActive(false);
    }

    public void StartGameOverScreen()
    {
        gameOver.color = gameOverStartColor;
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
    }
}
