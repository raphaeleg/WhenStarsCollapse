using System;
using UnityEngine;

[CreateAssetMenu]
public class ScoreManager : MonoBehaviour
{
    private int stars = 0;
    private int whiteDwarfs = 0;
    private int blackHoles = 0;

    public void Start() { Restart(); }

    public void AddCount_Star() { 
        stars++;
        EventManager.TriggerEvent("StarsText", stars);
    }
    public void AddCount_WhiteDwarf() { whiteDwarfs++; }
    public void AddCount_BlackHole() { 
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
