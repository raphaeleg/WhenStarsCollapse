using UnityEngine;
using System.Collections.Generic;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance = null;
    public static Dictionary<string, Action<int>> eventDictionary;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        eventDictionary = new Dictionary<string, Action<int>>();
    }

    public static void StartListening(string eventName, Action<int> listener)
    {

        Action<int> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<int> listener)
    {
        Action<int> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, int h)
    {
        Action<int> thisEvent = null;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke(h);
        }
    }
}