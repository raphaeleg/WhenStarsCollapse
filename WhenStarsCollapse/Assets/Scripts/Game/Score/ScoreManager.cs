using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Score : ScriptableObject
{
    public int stars = 0;
    public int blackHoles = 0;
    public int whiteDwarfs = 0;
    public int time = 0;
    public void Restart()
    {
        stars = 0;
        whiteDwarfs = 0;
        blackHoles = 0;
        time = 0;
    }
}