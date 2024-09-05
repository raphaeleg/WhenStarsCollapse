using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Gameplay_StatsUI : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text starsText;
    [SerializeField] TMP_Text blackHolesText;
    private Dictionary<string, Action<int>> SubscribedEvents;

    private void Awake()
    {
        SubscribedEvents = new() {
            { "TimerText", Event_UpdateTimerText },
            { "BlackHoleText", Event_UpdateBlackHoleText },
            { "StarsText", Event_UpdateStarsText },
            { "RestartStats", Event_SetToZero },
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

    public void Event_UpdateTimerText(int val) 
    {
        int minute = val / 60;
        string minuteStr = minute.ToString();
        if (minuteStr.Length == 1) { minuteStr = "0" + minuteStr; }

        int second = val % 60;
        string secondStr = second.ToString();
        if (secondStr.Length == 1) { secondStr = "0" + secondStr; }
            
        timerText.text = minuteStr + ":" + secondStr; 
    }
    public void Event_UpdateStarsText(int val) 
    {
        string formattedString = val.ToString();
        if (formattedString.Length == 1) { formattedString = "0" + formattedString; }
        starsText.text = formattedString; 
    }
    public void Event_UpdateBlackHoleText(int val) 
    {
        string formattedString = val.ToString();
        if (formattedString.Length == 1) { formattedString = "0" + formattedString; }
        blackHolesText.text = formattedString;
    }
    public void Event_SetToZero(int val)
    {
        timerText.text = "00:00";
        starsText.text = "00";
        blackHolesText.text = "00";
    }
}
