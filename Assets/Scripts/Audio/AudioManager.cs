using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixerGroup sfxAudioMixer;

    [SerializeField]
    Sound[] m_sounds;

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

        for (int i = 0; i < m_sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + m_sounds[i].m_name.ToString());
            go.transform.SetParent(transform);
            m_sounds[i].SetSource(go.AddComponent<AudioSource>());
            AudioSource audioSource = go.GetComponent<AudioSource>();

            audioSource.outputAudioMixerGroup = sfxAudioMixer;
            audioSource.spatialBlend = .5f;

        }
    }

    private Sound GetSound(SoundName name)
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].m_name == name)
            {
                return m_sounds[i];
            }
        }

        return null;
    }


    public void StopSound(SoundName name)
    {
        Sound requestedSound = GetSound(name);
        if (requestedSound == null)
        {
            Debug.LogWarning("AudioManager: Sound name not found on list: " + name.ToString());
        }
        else
        {
            requestedSound.Stop();
        }
    }

    public void PlaySound(SoundName name)
    {
        Sound requestedSound = GetSound(name);
        if (requestedSound == null)
        {
            Debug.LogWarning("AudioManager: Sound name not found on list: " + name.ToString());
        }
        else
        {
            requestedSound.Play();
        }
    }

    public void PlaySoundSpacially(SoundName name, Vector3 position)
    {
        Sound requestedSound = GetSound(name);
        if (requestedSound == null)
        {
            Debug.LogWarning("AudioManager: Sound name not found on list: " + name.ToString());
        }
        else
        {
            //print("Playing " + name.ToString());
            requestedSound.PlaySpacially(position);
        }
    }


}


[System.Serializable]
public class Sound
{
    public SoundName m_name;
    public AudioClip[] m_clips;

    [Range(0f, 1f)]
    public float volume = 1.0f;
    [Range(0f, 3f)]
    public float pitch = 1.0f;
    public bool loop;
    public Vector2 m_randomVolumeRange = new Vector2(1.0f, 1.0f);
    public Vector2 m_randomPitchRange = new Vector2(1.0f, 1.0f);

    private AudioSource m_source;

    public void SetSource(AudioSource source)
    {
        m_source = source;
        m_source.loop = loop;
        int randomClip = Random.Range(0, m_clips.Length - 1);
        m_source.clip = m_clips[randomClip];
    }

    public void Play()
    {
        if (m_clips.Length > 1)
        {
            int randomClip = Random.Range(0, m_clips.Length);
            m_source.clip = m_clips[randomClip];
        }
        m_source.spatialBlend = 0;
        m_source.volume = volume * Random.Range(m_randomVolumeRange.x, m_randomVolumeRange.y);
        m_source.pitch = pitch * Random.Range(m_randomPitchRange.x, m_randomPitchRange.y);
        m_source.PlayOneShot(m_source.clip, m_source.volume);
    }

    public void Stop()
    {
        m_source.Stop();
    }

    public void PlaySpacially(Vector3 position)
    {
        if (m_clips.Length > 1)
        {
            int randomClip = Random.Range(0, m_clips.Length);
            m_source.clip = m_clips[randomClip];
        }
        m_source.volume = volume * Random.Range(m_randomVolumeRange.x, m_randomVolumeRange.y);
        m_source.pitch = pitch * Random.Range(m_randomPitchRange.x, m_randomPitchRange.y);
        m_source.transform.position = position;
        m_source.spatialBlend = 1;
        m_source.Play();
    }

}

public enum SoundName
{
    PlayerDead,
    PlayerAttacked,
    ZombieDead,
    ZombieAttacked,
    ZombieNoises,
    CoinPickup,

    PistolFire,
    PistolReload,
    ShotgunFire,
    ShotgunReload,
    MGFire,
    MGReload,
}