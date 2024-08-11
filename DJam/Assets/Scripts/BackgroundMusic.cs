using System;
using UnityEngine;
using UnityEngine.UI;
using AK.Wwise;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance = null;

    public float soundEffects = 0.5f;
    public float musicVolume = 0.5f;

    public bool sfxMute = false;
    public bool bgmMute = false;

    public Image sfxImage;
    public Image bgmImage;
    public Sprite sfxNotMuteImage;
    public Sprite sfxMuteImage;
    public Sprite bgmNotMuteImage;
    public Sprite bgmMuteImage;

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
        if (!sfxMute)
        {
            soundEffects = sfxSlider.value;
            AkSoundEngine.SetRTPCValue("SFX_Volume", 100 * sfxSlider.value);
        }
    }

    public void MusicVolumeChanged()
    {
        if (!bgmMute)
        {
            musicVolume = volumeSlider.value;
            AkSoundEngine.SetRTPCValue("Music_Volume", 100 * volumeSlider.value);
        }
    }

    public void SoundEffectsMuted()
    {
        sfxMute = !sfxMute;
        if (sfxMute)
        {
            sfxImage.sprite = sfxMuteImage;
            AkSoundEngine.SetRTPCValue("SFX_Volume", 0);
        }
        else
        {
            sfxImage.sprite = sfxNotMuteImage;
            AkSoundEngine.SetRTPCValue("SFX_Volume", 100 * sfxSlider.value);
        }
    }

    public void MusicVolumeMuted()
    {
        bgmMute = !bgmMute;
        if (bgmMute)
        {
            bgmImage.sprite = bgmMuteImage;
            AkSoundEngine.SetRTPCValue("Music_Volume", 0);
        }
        else
        {
            bgmImage.sprite = bgmNotMuteImage;
            AkSoundEngine.SetRTPCValue("Music_Volume", 100 * volumeSlider.value);
        }
    }

    public void AudioSettingsSetup()
    {
        if (sfxMute)
            sfxImage.sprite = sfxMuteImage;
        else
            sfxImage.sprite = sfxNotMuteImage;

        if (bgmMute)
            bgmImage.sprite = bgmMuteImage;
        else
            bgmImage.sprite = bgmNotMuteImage;

        sfxSlider.value = soundEffects;
        volumeSlider.value = musicVolume;
    }

    /// <summary>
    /// Play an SFX, with or without a source.
    /// </summary>
    /// <param name="sfxName">
    /// The name of the SFX. Go to Window > Wwise Picker for a list of all sfx names (events).
    /// </param>
    public void PlaySFX(string sfxName)
    {
        Debug.Log("Called");
        AkSoundEngine.PostEvent(sfxName, gameObject);
    }
}