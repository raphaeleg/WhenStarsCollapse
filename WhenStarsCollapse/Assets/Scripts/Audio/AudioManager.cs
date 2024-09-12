using FMODUnity;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    // public enum MusicArea { CALM_AREA = 0, CHAOTIC_AREA = 1, LOST_AREA = 2 }
    private List<EventInstance> eventInstances = new List<EventInstance>();
    private List<EventInstance> eventInstances_Game = new List<EventInstance>();
    private EventInstance ambienceEventInstance;
    private EventInstance musicEventInstance;
    private void Awake()
    {
        if (instance != null)
        {
            //Debug.LogError("Found more than one Audio Manager in scene");
            Destroy(gameObject);
        }
        instance = this;
    }
    private void Start()
    {
        InitializeMusic(FMODEvents.instance.BG);
    }

    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound, Vector3.zero);
    }

    public void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateEventInstance(ambienceEventReference, true);
        ambienceEventInstance.start();
    }
    public void SetAmbienceParameter(string paremeterName, float value)
    {
        ambienceEventInstance.setParameterByName(paremeterName, value);
    }
    public void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }
    public void SetMusicArea(float area)
    {
        musicEventInstance.setParameterByName("area", area);
    }
    public EventInstance CreateEventInstance(EventReference eventReference, bool isGameEvent = false)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        if (isGameEvent) { eventInstances_Game.Add(eventInstance); }
        else { eventInstances.Add(eventInstance); }
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(eventInstance, GetComponent<Transform>(), GetComponent<Rigidbody>());
        return eventInstance;
    }
    public void CleanGameInstances()
    {
        foreach (EventInstance ei in eventInstances_Game)
        {
            ei.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ei.release();
        }
    }
    private void OnDestroy()
    {
        foreach (EventInstance ei in eventInstances)
        {
            ei.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            ei.release();
        }
    }
}
