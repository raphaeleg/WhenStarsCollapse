using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScoreManager : ScriptableObject
{
    public int stars = 0;
    public int blackHoles = 0;
    public int whiteDwarfs = 0;
    public int time = 0;
    #region EventManager
    private Dictionary<string, Action<int>> SubscribedEvents;

    private void Awake()
    {
        SubscribedEvents = new() {
            { "TimerText", Event_AddCount_Timer },
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
    #endregion
    public void Event_AddCount_Timer(int count) { time = count; }
    public void Restart()
    {
        stars = 0;
        whiteDwarfs = 0;
        blackHoles = 0;
        //EventManager.TriggerEvent("RestartStats", 0);
    }
}