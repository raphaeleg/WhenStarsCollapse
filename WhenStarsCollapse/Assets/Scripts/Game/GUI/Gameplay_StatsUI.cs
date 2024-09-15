using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// Updates the text at the top-left detailing the statistics of the game.
/// </summary>
public class Gameplay_StatsUI : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text starsText;
    [SerializeField] TMP_Text blackHolesText;

    #region Event Listeners
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
    #endregion

    private void Event_UpdateTimerText(int val) 
    {
        string minuteStr = FormatNumber(val / 60);
        string secondStr = FormatNumber(val % 60);

        timerText.text = minuteStr + ":" + secondStr; 
    }
    private void Event_UpdateStarsText(int val) 
    {
        string formattedString = FormatNumber(val);
        starsText.text = formattedString; 
    }
    private void Event_UpdateBlackHoleText(int val) 
    {
        string formattedString = FormatNumber(val);
        blackHolesText.text = formattedString;
    }
    private void Event_SetToZero(int val)
    {
        timerText.text = "00:00";
        starsText.text = "00";
        blackHolesText.text = "00";
    }
    private string FormatNumber(int val)
    {
        string formattedString = val.ToString();
        if (formattedString.Length == 1)
        {
            formattedString = "0" + formattedString;
        }
        return formattedString;
    }
}
