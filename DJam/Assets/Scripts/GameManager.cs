using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Planet
    [SerializeField] List<GameObject> PlanetPrefabs;
    const float TIMER_PLANET = 5f;

    // Canvas
    public Transform parentTarget;
    public RectTransform canvas;
    float maxWidth = 0;
    float maxHeight = 0;
    private const float PADDING = 100;
    private const float UI_WIDTH = 70;

    private float localTimer;
    [SerializeField] private SceneLoader sceneLoader;

    private const int MAX_SPAWN_ATTEMPTS = 5;
    [SerializeField] private int spawnAttempts = 0;

    private bool lostGame = false;

    [SerializeField] HighScore highScore;

    private void Start()
    {
        lostGame = false;
        localTimer = TIMER_PLANET;
        maxWidth = canvas.rect.width + canvas.rect.x;
        maxHeight = canvas.rect.height + canvas.rect.y;
        GenerateNewPlanet();
        StartCoroutine(PerSecond());
    }

    private void Update()
    {
        UpdateTimer();

        if (localTimer > 0) { return; }

        GenerateNewPlanet(); 
        localTimer = TIMER_PLANET;
    }
    private void UpdateTimer() { if (localTimer > 0) { localTimer -= Time.deltaTime; }  }

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
        // Available space: 
        var x = Random.Range(-maxWidth + PADDING, maxWidth - PADDING);
        var y = Random.Range(-maxHeight + UI_WIDTH, maxHeight - PADDING);
        Debug.Log(x + "" + y);
        while ((x > 500 & y < -250) || (x < -250 && y > 250) || (x > 550 && y > 250))
        {
            Debug.Log("rerandomize");
            x = Random.Range(-maxWidth + PADDING, maxWidth - PADDING);
            y = Random.Range(-maxHeight + UI_WIDTH, maxHeight - PADDING);
        }
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
    }

    IEnumerator GenNewPlanetCooldown()
    {
        yield return new WaitForSeconds(0.2f*spawnAttempts);
        GenerateNewPlanet();
    }

    public void RestartSpawnAttempts() { spawnAttempts = 0; }

    IEnumerator PerSecond()
    {
        yield return new WaitForSeconds(1);
        while (this.enabled == true)
        {
            highScore.timer++;
            yield return new WaitForSeconds(1);
        }
    }
}
