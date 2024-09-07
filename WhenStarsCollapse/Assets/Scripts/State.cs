using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Planets
{
    public abstract class State
    {
        protected Planet Planet;
        public State(Planet planet)
        {
            Planet = planet;
        }

        public virtual IEnumerator Start()
        {
            yield break;
        }
        public virtual IEnumerator Update()
        {
            yield break;
        }
        public virtual IEnumerator Heal()
        {
            yield break;
        }

        public virtual void Collided(Collider2D other) {
            return;
        }

        public virtual IEnumerator Shrink(GameObject collider)
        {
            const float INTERVAL = 0.01f;

            Vector3 scale = Planet.transform.localScale;
            float ratio = scale.x/scale.y;
            Vector3 scaleStep = new(INTERVAL*ratio, INTERVAL, 0);
            
            while (Planet.visuals.IsGreaterThan(INTERVAL))
            {
                Planet.visuals.ShrinkBy(scaleStep);
                Planet.visuals.MoveTowards(collider.transform.localPosition,INTERVAL);
                yield return new WaitForSeconds(INTERVAL);
            }
            Planet.OnDestroy();
        }
    }
}

/*
Planet
- Sprite overlays
- particle system
*/