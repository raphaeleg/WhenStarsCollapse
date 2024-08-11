using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Planet
    [SerializeField] List<GameObject> PlanetPrefabs;
    [SerializeField] PlanetRuntimeSet PlanetList;
    const float TIMER_PLANET = 10f;

    // Canvas
    public Transform parentTarget;
    public RectTransform canvas;
    float maxWidth = 0;
    float maxHeight = 0;
    private const float padding = 50;

    [SerializeField] private float localTimer;
    [SerializeField] private SceneLoader sceneLoader;


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
        int rand = Random.Range(0,PlanetPrefabs.Count);
        GameObject planet = Instantiate(PlanetPrefabs[rand]);
        planet.transform.SetParent(parentTarget);
        planet.transform.localPosition = GenerateRandomPosition();
    }

    private Vector2 GenerateRandomPosition()
    {
        var x = Random.Range(-maxWidth + padding, maxWidth - padding);
        var y = Random.Range(-maxHeight + padding, maxHeight - padding);
        return new Vector2(x, y);
    }

    public void CheckLoseCondition()
    {
        int currentBlackHoles = GameObject.Find("ScoreManager").GetComponent<HighScore>().blackHoles;
        if (currentBlackHoles >= 6)
        {
            sceneLoader.LoadWinScene();
        }
    }
}
