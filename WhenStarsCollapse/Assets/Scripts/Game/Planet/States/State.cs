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
        public virtual IEnumerator Collided(Collider2D other)
        {
            yield break;
        }

        public virtual void ShrinkUntilDestroy(GameObject collider)
        {
            Planet.GetComponent<Anim_Shrink>().ShrinkUntilDestroy(collider);
        }
    }
}