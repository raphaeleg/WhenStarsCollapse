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
        private const float MIN_DIR = 0.2f;

        private void Start() {
            StartCoroutine("InfiniteSpawn");
        }

        private IEnumerator InfiniteSpawn(){
            while(true) {
                GameObject meteor = Instantiate(MeteorPrefab);
                meteor.transform.SetParent(gameObject.transform);
                InitializePoints(meteor.GetComponent<Meteor>());

                yield return new WaitForSeconds(SPAWN_INTERVALS);
            }
        }

        private void InitializePoints(Meteor m) {
            // origin
            int rndSide = Random.Range(0,2);
            int rndExtreme = Random.Range(0,2);

            Bounds bound = BoxCollider.bounds;
            float rndX = Random.Range(0,bound.max.x);
            float rndY = Random.Range(0,bound.max.y);
            float dirRangeClamp = Random.Range(-1,-MIN_DIR);
            float dirRange = Random.Range(-1,1);
            if (dirRange == 0) { dirRange = MIN_DIR; }

            Vector2 rndOrigin = Vector2.zero;
            Vector2 rndDir = Vector2.zero;
            
            switch(rndSide){
                case 0:
                    float extremeX = bound.max.x;
                    if (rndExtreme == 0) { 
                        extremeX = bound.min.x; 
                        dirRangeClamp = -dirRangeClamp;
                    }
                    rndOrigin = new(extremeX,rndY);
                    rndDir = new(dirRangeClamp, dirRange);
                    break;
                default:
                    float extremeY = bound.max.y;
                    if (rndExtreme == 0) { 
                        extremeY = bound.min.y; 
                        dirRangeClamp = -dirRangeClamp;
                    }
                    rndOrigin = new(rndX,extremeY);
                    rndDir = new(dirRange, dirRangeClamp);
                    break;
            }
            m.Init(rndOrigin, rndDir.normalized);
        }
    }
}