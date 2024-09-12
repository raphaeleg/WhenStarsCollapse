using FMODUnity;
using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance { get; private set; }
    [field: Header("Music")]
    [field: SerializeField] public EventReference BG { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference Ambience_BlackHole { get; private set; }

    [field: Header("SFX")]
    [field: SerializeField] public EventReference ButtonClick_Default { get; private set; }
    [field: SerializeField] public EventReference ButtonClick_Negative { get; private set; }
    [field: SerializeField] public EventReference MeteorCollected { get; private set; }

    [field: Header("Gameplay")]
    [field: SerializeField] public EventReference Planet { get; private set; }
    [field: SerializeField] public EventReference MeteorCollected_Max { get; private set; }
    [field: SerializeField] public EventReference Rune_Click { get; private set; }
    [field: SerializeField] public EventReference Rune_Made { get; private set; }
    [field: SerializeField] public EventReference SFX_Correct { get; private set; }
    [field: SerializeField] public EventReference Explode { get; private set; }

    #region Event Listeners
    private Dictionary<string, Action<int>> SubscribedEvents = new();
    private void Awake()
    {
        if (instance != null)
        {
            //Debug.LogError("Found more than one FMOD Events in scene");
            Destroy(gameObject);
            return;
        }
        instance = this;
        SubscribedEvents = new() {
                { "SFX_ButtonClick", SFX_ButtonClick },
                { "SFX_Typewriter", SFX_Typewriter },
                { "SFX_ScoreAppear", SFX_Typewriter },
                { "isSuccessfulSpawn", SFX_Planet },
                { "Rune_SetDragging", SFX_Rune },
                { "AddCure", SFX_RuneMade },
                { "CollectMeteor", SFX_MeteorCollected },
                { "BlackHoleText", Loop_BlackHole },
                { "Lose", ChangeArea },
                { "ChangeMusicArea", ChangeArea },
            };
    }

    private void OnEnable()
    {
        StartCoroutine("DelayedSubscription");
    }
    private IEnumerator DelayedSubscription()
    {
        yield return new WaitForSeconds(0.0001f);
        foreach (var pair in SubscribedEvents)
        {
            EventManager.StartListening(pair.Key, pair.Value);
        }
    }

    private void OnDisable()
    {
        foreach (var pair in SubscribedEvents)
        {
            EventManager.StopListening(pair.Key, pair.Value);
        }
    }
    #endregion
    // drag change, typewriter change, highscore add
    
    public void SFX_ButtonClick(int val)
    {
        // 0 = default, 1 = negative (quit, pause, scene transitions, typewriter)
        if (val == 0 ) { AudioManager.instance.PlayOneShot(ButtonClick_Default); }
        else { AudioManager.instance.PlayOneShot(ButtonClick_Negative); }
    }
    public void SFX_Typewriter(int val)
    {
        AudioManager.instance.PlayOneShot(MeteorCollected);
    }
    public void SFX_Rune(int val)
    {
        // 0 = end drag
        if (val != 0) { AudioManager.instance.PlayOneShot(MeteorCollected_Max); }
    }
    public void SFX_RuneMade(int val)
    {
        AudioManager.instance.PlayOneShot(Rune_Made);
    }
    public void SFX_Planet(int val)
    {
        // spawn, sick, get big
        AudioManager.instance.PlayOneShot(Planet);
    }
    public void Correct(int val)
    {
        AudioManager.instance.PlayOneShot(SFX_Correct);
    }
    public void SFX_MeteorCollected(int val)
    {
        // 0 = default, 1 = special
        if (val == 0) { AudioManager.instance.PlayOneShot(MeteorCollected); }
        else { AudioManager.instance.PlayOneShot(MeteorCollected_Max); }
    }
    public void Loop_BlackHole(int val)
    {
        AudioManager.instance.PlayOneShot(Explode);
        if (val == 0) { AudioManager.instance.InitializeAmbience(Ambience_BlackHole); }
        else { AudioManager.instance.SetAmbienceParameter("blackhole_intensity", val); }
    }
    public void ChangeArea(int area)
    {
        if (area == 2) { AudioManager.instance.CleanGameInstances(); }
        AudioManager.instance.SetMusicArea((float)area);
    }
}
