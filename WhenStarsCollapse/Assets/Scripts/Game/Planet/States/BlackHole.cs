using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meteors;

namespace Planets
{
    /// <summary>
    /// Handles a Black Hole object behaviour.
    /// </summary>
    public class BlackHole : State
    {
        public BlackHole(Planet planet) : base(planet) { }

        public override IEnumerator Start()
        {
            Planet.visuals.Anim_BecomeBlackHole();
            Planet.GetComponent<Anim_Shrink>().DisableShrink();
            yield return new WaitForSeconds(0.5f);

            EventManager.TriggerEvent("blackHoleSpawn", 0);
        }
        public override IEnumerator Collided(Collider2D other)
        {
            Anim_Shrink otherShrinkAnim = other.GetComponent<Anim_Shrink>();
            if (other.CompareTag("Planet"))
            {
                // Make sure ShrinkUntilDestroy only plays in valid Planet States.
                if (!otherShrinkAnim.IsShrinking()) 
                { 
                    Planet.visuals.Anim_Eat(); 
                }
                other.GetComponent<Planet>().ShrinkUntilDestroy(Planet.gameObject);
            }
            else if (otherShrinkAnim)
            {
                if (!otherShrinkAnim.IsShrinking()) 
                { 
                    Planet.visuals.Anim_Eat(); 
                }
                otherShrinkAnim.ShrinkUntilDestroy(Planet.gameObject);
            }
            yield break;
        }

        public override void ShrinkUntilDestroy(GameObject collider)
        {
            return;
        }
    }
}