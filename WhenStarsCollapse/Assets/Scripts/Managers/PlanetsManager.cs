using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages planet spawning
public class PlanetsManager : MonoBehaviour
{
    public PolygonCollider2D polygonCollider;
    public GameObject PlanetPrefab;
    private const int SPAWN_INTERVALS = 2;

    #region Event Listeners
    private Dictionary<string, Action<int>> SubscribedEvents;

    private void Awake()
    {
        SubscribedEvents = new() {
            { "isUnsuccessfulSpawn", SpawnOnce }
        };
    }

    private void OnEnable()
    {
        foreach (var pair in SubscribedEvents)
        {
            EventManager.StartListening(pair.Key, pair.Value);
        }
    }

    private void OnDisable()
    {
        foreach (var pair in SubscribedEvents)
        {
            EventManager.StopListening(pair.Key, pair.Value);
        }
    }
    #endregion

    private void Start()
    {
        if (polygonCollider == null) { GetComponent<PolygonCollider2D>(); }
        if (polygonCollider == null) { Debug.Log("Please assign PolygonCollider2D component."); }

        StartCoroutine("InfiniteSpawn");
    }

    private IEnumerator InfiniteSpawn(){
        while(true) {
            StartCoroutine("SpawnOnce",0);

            yield return new WaitForSeconds(SPAWN_INTERVALS);
        }
    }

    private void SpawnOnce(int val){
        bool validPoint = false;
        Vector2 rndPoint2D = Vector2.zero;
        while(!validPoint) {
            rndPoint2D = RandomPointInBounds(polygonCollider.bounds, 1f);
            Vector2 rndPointInside = polygonCollider.ClosestPoint(rndPoint2D);
            
            validPoint = (rndPointInside.x == rndPoint2D.x && rndPointInside.y == rndPoint2D.y);
        }
        GameObject planet = Instantiate(PlanetPrefab);
        planet.transform.SetParent(gameObject.transform);
        planet.transform.position = rndPoint2D;
    }

    private Vector2 RandomPointInBounds(Bounds bounds, float scale)
    {
        return new Vector2(
            UnityEngine.Random.Range(bounds.min.x * scale, bounds.max.x * scale),
            UnityEngine.Random.Range(bounds.min.y * scale, bounds.max.y * scale)
        );
    }
}
