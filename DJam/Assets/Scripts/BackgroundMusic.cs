using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource musicSource;
    public AudioClip mainMenuBGM;
    public AudioClip tutorialBGM;
    public AudioClip gameplayBGM;
    public AudioClip winBGM;

    private static BackgroundMusic instance = null;

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
    public void PlayTutorialBGM() { musicSource.Stop(); musicSource.clip = tutorialBGM; musicSource.Play(); }
    public void PlayGameplayBGM() { musicSource.Stop(); musicSource.clip = gameplayBGM; musicSource.Play(); }
    public void PlayWinBGM() { musicSource.Stop(); musicSource.clip = winBGM; musicSource.Play(); }
}