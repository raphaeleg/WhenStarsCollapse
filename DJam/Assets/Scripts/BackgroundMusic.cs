using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class BackgroundMusic : MonoBehaviour
{
    // BGM
    private AudioSource musicSource;
    public AudioClip mainMenuBGM;
    public AudioClip gameplayBGM;
    public AudioClip gameOverBGM;

    public AudioSource[] SFX; // Points to SFXs in scene

    private static BackgroundMusic instance = null;

    public float soundEffects = 0.5f;
    public float musicVolume = 0.5f;

    // Sliders
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider volumeSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayMainMenuBGM() { musicSource.Stop(); musicSource.clip = mainMenuBGM; musicSource.Play(); }
    public void PlayGameplayBGM() { musicSource.Stop(); musicSource.clip = gameplayBGM; musicSource.Play(); }
    public void PlayGameOverBGM() { musicSource.Stop(); musicSource.clip = gameOverBGM; musicSource.Play(); }

    public void SoundEffectsChanged()
    {
        soundEffects = sfxSlider.value;
    }

    public void MusicVolumeChanged()
    {
        musicVolume = volumeSlider.value;
    }

    private void Update()
    {
        for (int i = 0; i < SFX.Length; i++)
        {
            SFX[i].volume = soundEffects;
        }
        //musicSource.volume = musicVolume;
    }
}