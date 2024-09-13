using FMODUnity;
using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance { get; private set; }

    #region EventReference Variables
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
    #endregion

    #region Event Listeners
    private Dictionary<string, Action<int>> SubscribedEvents = new();
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        SubscribedEvents = new() {
                { "SFX_ButtonClick", SFX_ButtonClick },
                { "SFX_Typewriter", SFX_Click },
                { "SFX_ScoreAppear", SFX_Click },

                { "isSuccessfulSpawn", SFX_Planet },
                { "whiteDwarfSpawn", SFX_Explode },
                { "Rune_SetDragging", SFX_Rune },
                { "CollectMeteor", SFX_MeteorCollected },
                { "AddCure", SFX_RuneMade },
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
    
    private void Play(EventReference eventRef) { AudioManager.Instance.PlayOneShot(eventRef); }
    private void SFX_Click(int val = 0) { Play(MeteorCollected); }
    private void SFX_ButtonClick(int val = 0)
    {
        if (val == 0 ) { Play(ButtonClick_Default); }   // default
        else { Play(ButtonClick_Negative); }            // negative / special (quit, pause, scene transitions, typewriter)
    }
    private void Correct(int val = 0) { Play(SFX_Correct); }
    private void SFX_Planet(int val = 0) { Play(Planet); }   // spawn, sick, get big
    private void SFX_Explode(int val = 0) { Play(Explode); }
    private void SFX_Rune(int val = 0)
    {
        if (val != 0) { Play(MeteorCollected_Max); }    // 0 = end drag
    }
    private void SFX_RuneMade(int val = 0)
    {
        if (val == -1) { Correct(); }    // Used Rune
        else { Play(Rune_Made); }           // Made Rune
    }
    private void SFX_MeteorCollected(int val = 0)
    {
        if (val == 2) { Play(MeteorCollected_Max); }    
        else { SFX_Click(); }
    }
    private void Loop_BlackHole(int val = 0)
    {
        SFX_Explode();
        if (val == 0) { AudioManager.Instance.InitializeAmbience(Ambience_BlackHole); }
        else { AudioManager.Instance.SetAmbienceParameter("blackhole_intensity", val); }
    }
    private void ChangeArea(int area)
    {
        if (area == 2) { AudioManager.Instance.CleanGameInstances(); }
        AudioManager.Instance.SetMusicArea((Audio_MusicArea)area);
    }
}
