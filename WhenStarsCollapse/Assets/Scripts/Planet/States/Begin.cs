using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    public class Begin : State
    {
        private const int VISIBILITY_DELAY = 1;
        private bool sentTrigger = false;

        public Begin(Planet planet) : base(planet) { }
        public override IEnumerator Start()
        {
            yield return new WaitForSeconds(VISIBILITY_DELAY);

            if (sentTrigger) { yield break; }
            sentTrigger = true;

            EventManager.TriggerEvent("isSuccessfulSpawn", 0);
            Planet.visuals.gameObject.SetActive(true);
            Planet.gameObject.GetComponent<Anim_Shrink>().enableShrink();
            yield return new WaitForSeconds(VISIBILITY_DELAY);
            Planet.visuals.SetPlanetType(Planet.faction.IntType());
            Planet.SetState(new Sick(Planet));
        }

        public override IEnumerator Collided(Collider2D other)
        {
            if (other.GetComponent<Planet>() == null) { yield break; }
            if (sentTrigger) { yield break; }
            sentTrigger = true;

            EventManager.TriggerEvent("isUnsuccessfulSpawn", 0);
            Planet.OnDestroy();
        }

        public override void ShrinkUntilDestroy(GameObject collider)
        {
            if (sentTrigger) { return; }
            sentTrigger = true;

            EventManager.TriggerEvent("isUnsuccessfulSpawn", 0);
            base.ShrinkUntilDestroy(collider);
        }
    }
}