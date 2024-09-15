using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    /// <summary>
    /// Handles the position and frequency of Planet Instantiation.
    /// </summary>
    public class PlanetsManager : MonoBehaviour
    {
        [SerializeField] GameObject PlanetPrefab;
        private PolygonCollider2D polygonCollider;
        private float SPAWN_INTERVALS = 5f;

        #region Event Listeners
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            SubscribedEvents = new() {
                { "isUnsuccessfulSpawn", SpawnOnce },
                { "DifficultyIncrease", IncreaseFrequency },
            };
        }

        private void OnEnable()
        {
            polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
            foreach (var pair in SubscribedEvents)
            {
                EventManager.StartListening(pair.Key, pair.Value);
            }
            StartCoroutine("InfiniteSpawn");
        }

        private void OnDisable()
        {
            foreach (var pair in SubscribedEvents)
            {
                EventManager.StopListening(pair.Key, pair.Value);
            }
            StopCoroutine("InfiniteSpawn");
        }
        #endregion

        private IEnumerator InfiniteSpawn()
        {
            while(true) 
            {
                StartCoroutine("SpawnOnce",0);

                yield return new WaitForSeconds(SPAWN_INTERVALS);
            }
        }

        private void SpawnOnce(int val)
        {
            Vector2 rndPoint2D = Vector2.zero;

            // Find a valid random position
            bool validPoint = false;
            while(!validPoint)
            {
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

        private void IncreaseFrequency(int val)
        {
            SPAWN_INTERVALS -= 0.5f;
        }
    }
}