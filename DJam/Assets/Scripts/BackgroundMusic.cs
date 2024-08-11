using System;
using UnityEngine;
using UnityEngine.UI;
using AK.Wwise;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance = null;

    public float soundEffects = 0.5f;
    public float musicVolume = 0.5f;

    // Sliders
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider volumeSlider;


    [SerializeField] private AK.Wwise.Event musicEvent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        musicEvent.Post(gameObject);
    }

    public void SoundEffectsChanged()
    {
        AkSoundEngine.SetRTPCValue("SFX_Volume", 100 * sfxSlider.value);
    }

    public void MusicVolumeChanged()
    {
        AkSoundEngine.SetRTPCValue("Music_Volume", 100 * volumeSlider.value);
    }

}