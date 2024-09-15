using System.Collections;
using UnityEngine;

namespace Planets
{
    /// <summary>
    /// Checks whether the newly-spawned planet is valid (not near a blackhole nor other planets).
    /// </summary>
    public class Begin : State
    {
        private const int VISIBILITY_DELAY = 1;
        private const int REMAINING_STATE_TIME = 1;
        private bool sentTrigger = false;
        public Begin(Planet planet) : base(planet) { }

        public override IEnumerator Start()
        {
            yield return new WaitForSeconds(VISIBILITY_DELAY);

            if (sentTrigger) 
            { 
                yield break; 
            }
            EventManager.TriggerEvent("isSuccessfulSpawn", 0);
            sentTrigger = true;

            Planet.visuals.gameObject.SetActive(true);
            Planet.gameObject.GetComponent<Anim_Shrink>().EnableShrink();

            yield return new WaitForSeconds(REMAINING_STATE_TIME);

            Planet.visuals.SetPlanetType(Planet.faction.IntType());
            Planet.SetState(new Sick(Planet));
        }

        public override IEnumerator Collided(Collider2D other)
        {
            if (other.GetComponent<Planet>() is null) 
            { 
                yield break; 
            }
            else if (sentTrigger)
            {
                yield break;
            }
            sentTrigger = true;

            EventManager.TriggerEvent("isUnsuccessfulSpawn", 0);
            Planet.OnDestroy();
        }

        public override void ShrinkUntilDestroy(GameObject collider)
        {
            if (sentTrigger) 
            { 
                return; 
            }
            sentTrigger = true;

            EventManager.TriggerEvent("isUnsuccessfulSpawn", 0);
            base.ShrinkUntilDestroy(collider);
        }
    }
}