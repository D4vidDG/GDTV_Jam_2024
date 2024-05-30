using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsFunctions : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider masterSlider, sfxSlider, musicSlider;
    
    public void SliderMoved(int whichSlider)
    {
        switch (whichSlider)
        {
            case 0:
                mixer.SetFloat("Master", Mathf.Log10(masterSlider.value) * 20);
                break;
            case 1:
                mixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
                break;
            case 2:
                mixer.SetFloat("Music", Mathf.Log10(musicSlider.value) * 20);
                break;
        }
    }
}
