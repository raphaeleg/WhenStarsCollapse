using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meteors 
{
    /// <summary>
    /// Handles the position and frequency of Meteor Instantiation.
    /// </summary>
    public class MeteorManager : MonoBehaviour
    {
        [SerializeField] GameObject MeteorPrefab;
        private BoxCollider2D BoxCollider;
        private float SPAWN_INTERVALS = 1f;
        #region Event Listeners
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            SubscribedEvents = new() {
                { "DifficultyIncrease", IncreaseFrequency },
            };
        }

        private void OnEnable()
        {
            BoxCollider = gameObject.GetComponent<BoxCollider2D>();
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

        private void IncreaseFrequency(int val) { SPAWN_INTERVALS -=0.01f; }

        private IEnumerator InfiniteSpawn(){
            while(true) 
            {
                GameObject meteor = Instantiate(MeteorPrefab);
                meteor.transform.SetParent(gameObject.transform);
                SetPosition(meteor.GetComponent<Meteor>());

                yield return new WaitForSeconds(SPAWN_INTERVALS);
            }
        }

        private void SetPosition(Meteor meteor) 
        {
            bool isSpawnY = UnityEngine.Random.value > 0.5f;
            bool isSpawnPositive = UnityEngine.Random.value > 0.5f;

            Bounds bound = BoxCollider.bounds;
            var rndX = UnityEngine.Random.Range(bound.min.x,bound.max.x);
            var rndY = UnityEngine.Random.Range(bound.min.y,bound.max.y);
            
            Vector2 generalSpawnLoc = Vector2.zero;

            switch(isSpawnY)
            {
                case false:     // instantiate at left/right
                    // isSpawnPositive == false -> at left
                    float extremeX = isSpawnPositive ? bound.max.x : bound.min.x;
                    generalSpawnLoc.x = isSpawnPositive ? 1 : -1;
                    meteor.Init(new(extremeX,rndY), generalSpawnLoc);
                    break;
                default:    // instantiate at top/bottom
                    // isSpawnPositive == false -> at bottom
                    float extremeY = isSpawnPositive ? bound.max.y : bound.min.y;
                    generalSpawnLoc.y = isSpawnPositive ? 1 : -1;
                    meteor.Init(new(rndX,extremeY), generalSpawnLoc);
                    break;
            }
        }
    }
}