using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderOnValueChangedSelector : MonoBehaviour
{
    private Slider slider;
    private SoundMixerManager soundMixerManager;

    void Start()
    {
        slider = GetComponent<Slider>();
        soundMixerManager = FindObjectOfType<SoundMixerManager>();

        if (slider != null && soundMixerManager != null)
        {
            if (gameObject.name.Contains("Master"))
            {
                slider.onValueChanged.AddListener(value => soundMixerManager.SetMasterVolume(value));
            }
            else if (gameObject.name.Contains("SoundFX"))
            {
                slider.onValueChanged.AddListener(value => soundMixerManager.SetSoundFXVolume(value));
            }
            else if (gameObject.name.Contains("Music"))
            {
                slider.onValueChanged.AddListener(value => soundMixerManager.SetMusicVolume(value));
            }
        }
        else
            Debug.LogError("Either the slider is not attached to the GO or no SoundMixerManager in the scene");
    }
}