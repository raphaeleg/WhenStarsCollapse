using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Planet
    [SerializeField] GameObject PlanetPrefab;
    [SerializeField] PlanetRuntimeSet PlanetList;
    const float TIMER_PLANET = 10f;

    // Canvas
    public Transform parentTarget;
    public RectTransform canvas;
    float maxWidth = 0;
    float maxHeight = 0;
    private const float padding = 50;

    [SerializeField] private float localTimer;

    private void Start()
    {
        localTimer = TIMER_PLANET;
        PlanetList.Clear();
        maxWidth = canvas.rect.width + canvas.rect.x;
        maxHeight = canvas.rect.height + canvas.rect.y;
        GenerateNewPlanet();
    }

    private void Update()
    {
        UpdateTimer();

        if (localTimer > 0) { return; }

        GenerateNewPlanet(); 
        localTimer = TIMER_PLANET;
    }
    private void UpdateTimer() { if (localTimer > 0) { localTimer -= Time.deltaTime; } }

    private void GenerateNewPlanet()
    {
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
}
