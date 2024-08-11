using System;
using UnityEngine;

[CreateAssetMenu]
public class HighScore : ScriptableObject
{
    public int timer = 0;
    public int stars = 0;
    public int whiteDwarfs = 0;
    public int blackHoles = 0;

    public void Start() { Restart(); }

    public void Restart()
    {
        timer = 0;
        stars = 0;
        whiteDwarfs = 0;
        blackHoles = 0;
    }
}
