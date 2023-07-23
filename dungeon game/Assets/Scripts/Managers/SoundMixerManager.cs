using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : Singleton<SoundMixerManager>
{
    [SerializeField]
    private AudioMixer audioMixer;
    private GameObject audioMenu;

    private void Start()
    {
        Hide();
    }

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
    }

    public void OpenAudioMenu()
    {
        audioMenu.SetActive(true);
    }

    public void CloseAudioMenu()
    {
        audioMenu.SetActive(false);
    }

    public void Hide()
    {
        if (audioMenu == null)
        {
            audioMenu = GameObject.FindWithTag("Audio");
        }
        audioMenu.SetActive(false);
    }
}