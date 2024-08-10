using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // States: Normal, Symptom, WhiteStar, BlackHole
    // Base WhiteStar Condition: be alive for 20 seconds AND have no symptoms at the end
    // Symptoms stop adding to planets once their 20 seconds have passed
    // Symptoms are added every 5 seconds -> Planets are in a Runtime list that game manager can add symptoms to a random one

    public const int Timer_Default = -1;
    public const int Timer_Symptom = 1;

}
