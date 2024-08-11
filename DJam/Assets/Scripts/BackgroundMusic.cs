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

    private void Start()
    {
        AkSoundEngine.PostEvent("Game_Start", gameObject);
        // AkSoundEngine.SetRTPCValue("Intensity", 100);
    }

    public void SoundEffectsChanged()
    {
        soundEffects = sfxSlider.value;
    }

    public void MusicVolumeChanged()
    {
        AkSoundEngine.SetRTPCValue("Volume", 100 * volumeSlider.value);
    }

}