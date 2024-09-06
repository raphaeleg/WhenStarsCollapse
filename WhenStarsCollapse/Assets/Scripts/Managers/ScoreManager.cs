using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int stars = 0;
    private int whiteDwarfs = 0;
    private int blackHoles = 0;
    private Dictionary<string, Action<int>> SubscribedEvents;

    private void Awake()
    {
        SubscribedEvents = new() {
            { "isSuccessfulSpawn", Event_AddCount_Star },
            { "blackHoleSpawn", Event_AddCount_BlackHole },
            { "whiteDwarfSpawn", Event_AddCount_WhiteDwarf },
        };
    }
    private void OnEnable()
    {
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

    
    public void Start() { Restart(); }

    public void Event_AddCount_Star(int val) { 
        stars++;
        EventManager.TriggerEvent("StarsText", stars);
    }
    public void Event_AddCount_WhiteDwarf(int val) { whiteDwarfs++; }
    public void Event_AddCount_BlackHole(int val) { 
        blackHoles++;
        EventManager.TriggerEvent("BlackHoleText", blackHoles);
    }
    private void Restart()
    {
        stars = 0;
        whiteDwarfs = 0;
        blackHoles = 0;
        EventManager.TriggerEvent("RestartStats", 0);
    }
}
