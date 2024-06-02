using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimedSound : MonoBehaviour
{
    public SoundName soundName;
    public float soundDelay, soundTimer;
    public float minDelay, maxDelay;

    void Start()
    {
        RandomDelay();
        soundTimer = 0;
    }

    private void Update()
    {
        soundTimer += Time.deltaTime;

        if (soundTimer >= soundDelay && !GameManager.instance.playerDead)
        {
            RandomDelay();
            AudioManager.instance.PlaySound(soundName);
            soundTimer = 0;
        }
        
    }

    private void RandomDelay()
    {
        soundDelay = Random.Range(minDelay, maxDelay);
    }
}