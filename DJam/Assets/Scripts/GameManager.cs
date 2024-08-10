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
    const float RATE_PLANET = 0.1f;

    // Canvas
    public Transform parentTarget;
    public RectTransform canvas;
    float maxWidth = 0;
    float maxHeight = 0;
    private const float padding = 50;

    // Symptom
    const float TIMER_SYMPTOM = 5f;
    private float timer_symptomSpawn = 5f;
    const float RATE_SYMPTOM = 0.1f;

    private void Start()
    {
        PlanetList.Clear();
        maxWidth = canvas.rect.width + canvas.rect.x;
        maxHeight = canvas.rect.height + canvas.rect.y;
        GenerateNewPlanet();
    }

    private void FixedUpdate()
    {
        timer_planetSpawn -= RATE_PLANET;
        if (timer_planetSpawn <= 0 ) { GenerateNewPlanet(); }

        timer_symptomSpawn -= RATE_SYMPTOM;
        if ( timer_symptomSpawn <= 0 ) { ApplySymptom(); }
    }

    private void GenerateNewPlanet()
    {
        timer_planetSpawn = TIMER_PLANET;
        GameObject planet = Instantiate(PlanetPrefab);
        planet.transform.SetParent(parentTarget);
        planet.transform.localPosition = GenerateRandomPosition();
    }

    private Vector2 GenerateRandomPosition()
    {
        var x = Random.Range(-maxWidth + padding, maxWidth - padding);
        var y = Random.Range(-maxHeight + padding, maxHeight - padding);
        return new Vector2(x, y);
    }

    private void ApplySymptom()
    {
        timer_symptomSpawn = TIMER_SYMPTOM;
        PlanetList.AddSymptomToRandom();
    }
}
