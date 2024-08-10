using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Planet
    [SerializeField] GameObject PlanetPrefab;
    [SerializeField] PlanetRuntimeSet PlanetList;
    const float TIMER_PLANET = 20f;
    private float timer_planetSpawn = 20f;
    private float rate_planetSpawn = 0.1f;

    

    // Symptom
    const float TIMER_SYMPTOM = 5f;
    private float timer_symptomSpawn = 5f;
    private float rate_symptomSpawn = 0.1f;

    private void Start()
    {
        PlanetList.Clear();
        GenerateNewPlanet();
    }

    private void FixedUpdate()
    {
        timer_planetSpawn -= rate_planetSpawn;
        if (timer_planetSpawn <= 0 ) { GenerateNewPlanet(); }

        timer_symptomSpawn -= rate_symptomSpawn;
        if ( timer_symptomSpawn <= 0 ) { ApplySymptom(); }
    }

    private void GenerateNewPlanet()
    {
        timer_planetSpawn = TIMER_PLANET;
        var p = Instantiate(PlanetPrefab);
        p.transform.position = GenerateRandomPosition();
    }

    private Vector2 GenerateRandomPosition()
    {
        int x = Random.Range(-8, 8);
        int y = Random.Range(-4, 4);
        return new Vector2(x, y);   // Make sure this returns a position within the screenspace
    }

    private void ApplySymptom()
    {
        timer_symptomSpawn = TIMER_SYMPTOM;
        int randomPlanetIndex = Random.Range(0,PlanetList.Planets.Count);
        var p = PlanetList.Planets[randomPlanetIndex];
        p.GetComponent<Planet>().AddSymptom();
        PlanetList.Planets[randomPlanetIndex] = p;
    }
}
