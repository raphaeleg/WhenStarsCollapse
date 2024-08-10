using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // States: Normal, Symptom, WhiteStar, BlackHole
    // Base WhiteStar Condition: be alive for 20 seconds AND have no symptoms at the end
    // Symptoms stop adding to planets once their 20 seconds have passed
    // Symptoms are added every 5 seconds -> Planets are in a Runtime list that game manager can add symptoms to a random one

    public const float TIMER_DEFAULT = -0.1f;
    public const float TIMER_SYMPTOM = 0.1f;
    public const float MAX_BALANCE = 100f;
    [SerializeField] float balance = 50f;
    [SerializeField] int currentSymptoms = 0;
    public enum PlanetStates {NORMAL, WHITEDWARF, BLACKHOLE};
    [SerializeField] PlanetStates state = PlanetStates.NORMAL;

    private void Update()
    {
        switch (state)
        {
            case PlanetStates.NORMAL:
                balance += CalculateNetBalance();
                if (balance >= MAX_BALANCE) { state = PlanetStates.BLACKHOLE; }
                else if (balance <= 0) { BecomeWhiteDwarf(); }
                break;
            case PlanetStates.WHITEDWARF:
                balance--;
                if (balance < 0) { 
                    // TODO
                }
                break;
            default:
                break;
        }
    }

    public void BecomeWhiteDwarf()
    {
        state = PlanetStates.WHITEDWARF;
        balance = 20;
    }

    private float CalculateNetBalance()
    {
        return TIMER_DEFAULT + TIMER_SYMPTOM * currentSymptoms;
    }

    public void AddSymptom() { currentSymptoms++; }
    public void Cure() { currentSymptoms--; }
}
