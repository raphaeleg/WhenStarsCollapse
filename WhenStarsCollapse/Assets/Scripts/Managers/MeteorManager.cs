using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meteors 
{
    public class MeteorManager : MonoBehaviour
    {
        [SerializeField] BoxCollider2D BoxCollider;
        [SerializeField] GameObject MeteorPrefab;
        private const int SPAWN_INTERVALS = 2;

        private void Start() {
            StartCoroutine("InfiniteSpawn");
        }

        private IEnumerator InfiniteSpawn(){
            while(true) {
                GameObject meteor = Instantiate(MeteorPrefab);
                meteor.transform.SetParent(gameObject.transform);
                SetPosition(meteor.GetComponent<Meteor>());

                yield return new WaitForSeconds(SPAWN_INTERVALS);
            }
        }

        private void SetPosition(Meteor meteor) {
            bool isSpawnY = Random.value > 0.5f;
            bool isSpawnPositive = Random.value > 0.5f;

            Bounds bound = BoxCollider.bounds;
            var rndX = Random.Range(bound.min.x,bound.max.x);
            var rndY = Random.Range(bound.min.y,bound.max.y);
            
            Vector2 generalSpawnLoc = Vector2.zero;

            switch(isSpawnY){
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