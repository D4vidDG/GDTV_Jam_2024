using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SequentialAudioClip : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public bool endLoop;

    private void OnEnable()
    {
        StartCoroutine(PlayAudioSequentially());
    }

    private void OnDisable()
    {
        StopCoroutine(PlayAudioSequentially());
        audioSource.Stop();
    }

    //i stole this, thanks stackoverflow
    IEnumerator PlayAudioSequentially()
    {
        yield return null;

        int loop = audioClips.Length;

        for (int i = 0; i < audioClips.Length; i++)
        {
            audioSource.clip = audioClips[i];
            audioSource.Play();

            while (audioSource.isPlaying)
            {
                yield return null;
            }
        }

        if (endLoop)
        {
            audioSource.clip = audioClips[audioClips.Length-1];
            audioSource.Play();
            audioSource.loop = true;
        }
    }
}
