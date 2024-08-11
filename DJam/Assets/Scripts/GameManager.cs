using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Planet
    [SerializeField] List<GameObject> PlanetPrefabs;
    const float TIMER_PLANET = 10f;

    // Canvas
    public Transform parentTarget;
    public RectTransform canvas;
    float maxWidth = 0;
    float maxHeight = 0;
    private const float padding = 50;

    private float localTimer;
    [SerializeField] private SceneLoader sceneLoader;

    private const int MAX_SPAWN_ATTEMPTS = 5;
    [SerializeField] private int spawnAttempts = 0;

    private bool lostGame = false;

    private void Start()
    {
        lostGame = false;
        localTimer = TIMER_PLANET;
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

    public void GenerateNewPlanet()
    {
        spawnAttempts++;
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
        Debug.Log("SpawnAttempt: " + spawnAttempts);
        if (spawnAttempts >= MAX_SPAWN_ATTEMPTS)
        {
            if (!lostGame)
            {
                lostGame = true;
                sceneLoader.LoadGameOverScene();
            }
        }
        StartCoroutine(GenNewPlanetCooldown());
        //Debug.Log("Detected Failed Planet. Spawn Attempts: " + spawnAttempts + " when previously: " + previousSpawnAttempt +" and confirmed successes: "+confirmSuccessfulSpawn);
    }

    IEnumerator GenNewPlanetCooldown()
    {
        yield return new WaitForSeconds(0.2f*spawnAttempts);
        GenerateNewPlanet();
    }

    public void RestartSpawnAttempts() { spawnAttempts = 0; }
}
