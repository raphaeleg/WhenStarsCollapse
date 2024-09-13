using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static UnityEditor.Progress;

public enum Audio_MusicArea { CALM = 0, CHAOTIC = 1, LOST = 2 }

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Event Instances")]
    private EventInstance eventInstance_Music;
    private EventInstance eventInstance_Ambience;
    private List<EventInstance> eventInstances = new();
    private List<EventInstance> eventInstances_Gameplay = new();

    [Header("Volume Control")]
    private List<VolumeController> VC = new();
    private VolumeController VC_SFX;
    public struct VolumeController
    {
        public VolumeType type;
        public float volume;
        public Bus bus;

        public VolumeController(VolumeType _type, float _value, Bus _bus)
        {
            type = _type;
            volume = _value;
            bus = _bus;
        }
    };

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        VC.Add(new(VolumeType.MUSIC, 1, RuntimeManager.GetBus("bus:/Music")));
        VC.Add(new(VolumeType.SFX, 1, RuntimeManager.GetBus("bus:/SFX")));

        InitializeMusic();
    }

    public void SetVolume(VolumeType type, float value)
    {
        int index = VC.FindIndex(item => item.type == type);
        if (index >= VC.Count) { return; }

        VolumeController vc = VC[index];
        vc.volume = value;
        vc.bus.setVolume(value);
        VC[index] = vc;
    }
    public float GetVolume(VolumeType type)
    {
        VolumeController vc = VC.First((item) => {  return item.type == type; });
        return vc.volume;
    }

    public void PlayOneShot(EventReference sound) { RuntimeManager.PlayOneShot(sound, Vector3.zero); }

    private void InitializeMusic()
    {
        eventInstance_Music = CreateEventInstance(FMODEvents.Instance.BG);
        eventInstance_Music.start();
    }
    public void SetMusicArea(Audio_MusicArea area) { 
        switch(area)
        {
            case (Audio_MusicArea.CALM):
                eventInstance_Music.setParameterByName("area", 0f);
                eventInstance_Music.setParameterByName("chaotic_intensity", 0f);
                break;
            case (Audio_MusicArea.CHAOTIC):
                eventInstance_Music.setParameterByName("area", 0f);
                eventInstance_Music.setParameterByName("chaotic_intensity", 1f);
                break;
            case (Audio_MusicArea.LOST):
                eventInstance_Music.setParameterByName("area", 1f);
                break;
            default:
                break;
        }
    }

    public void InitializeAmbience(EventReference eventRef)
    {
        eventInstance_Ambience = CreateEventInstance(eventRef, true);
        eventInstance_Ambience.start();
    }
    public void SetAmbienceParameter(string param, float value) { eventInstance_Ambience.setParameterByName(param, value); }
   
    public EventInstance CreateEventInstance(EventReference eventRef, bool isGameEvent = false)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventRef);
        if (isGameEvent) { eventInstances_Gameplay.Add(eventInstance); }
        else { eventInstances.Add(eventInstance); }
        RuntimeManager.AttachInstanceToGameObject(eventInstance, GetComponent<Transform>(), GetComponent<Rigidbody>());
        return eventInstance;
    }
    public void CleanGameInstances()
    {
        foreach (EventInstance ei in eventInstances_Gameplay)
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
