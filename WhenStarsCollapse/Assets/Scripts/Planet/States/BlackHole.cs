using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meteors;

namespace Planets
{
    public class BlackHole : State
    {
        public BlackHole(Planet planet) : base(planet) { }

        public override IEnumerator Start()
        {
            Planet.visuals.Anim_BecomeBlackHole();
            Planet.GetComponent<Anim_Shrink>().disableShrink();
            yield return new WaitForSeconds(0.5f);

            EventManager.TriggerEvent("blackHoleSpawn", 0);
        }
        public override void Collided(Collider2D other)
        {
            Anim_Shrink otherShrinkAnim = other.GetComponent<Anim_Shrink>();
            if (other.CompareTag("Planet") == true) {
                if (!otherShrinkAnim.isShrinking()) { Planet.visuals.Anim_Eat(); Debug.Log("Called"); }
                other.GetComponent<Planet>().ShrinkUntilDestroy(Planet.gameObject); 
            }
            else if (otherShrinkAnim) {
                if (!otherShrinkAnim.isShrinking()) { Planet.visuals.Anim_Eat(); Debug.Log("Called2"); }
                otherShrinkAnim.ShrinkUntilDestroy(Planet.gameObject); 
            }
        }

        public override void ShrinkUntilDestroy(GameObject collider)
        {
            return;
        }
    }
}