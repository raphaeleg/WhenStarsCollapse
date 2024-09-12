using UnityEngine;
using System.Collections.Generic;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance = null;
    public static Dictionary<string, Action<int>> eventDictionary;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
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
        if (instance == null) { return; }
        Action<int> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
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