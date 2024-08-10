using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEventSO : ScriptableObject
{
    private readonly List<GameEventListener> eventListeners = new();

    public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--) { 
            eventListeners[i].OnEventRaised(); 
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener)) { return; }
        eventListeners.Add(listener); 
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener)) { return; }
        eventListeners.Remove(listener);
    }
}
