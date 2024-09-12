using System.Collections;
using UnityEngine;
using Runes;

namespace Planets
{
    public class Sick : State
    {
        public Sick(Planet planet) : base(planet) { }

        public int stage = 1;
        public bool isCuring = false;
        private const int INTERVAL = 5;

        public override IEnumerator Start()
        {
            Planet.visuals.Anim_Sick();
            yield return new WaitForSeconds(INTERVAL);
            while (stage < 3 && stage > 0)
            {
                stage++;

                if (stage is 2) { Planet.visuals.Anim_GetBig(); }
                Planet.visuals.SickParticle(stage is 3);
                yield return new WaitForSeconds(INTERVAL);
            }
            if (stage < 3) { Planet.SetState(new BlackHole(Planet)); }
        }
        private bool cureValidChecking(Rune rune)
        {
            return rune && Planet.faction.CompareType(rune.faction) && !isCuring;
        }
        public override IEnumerator Collided(Collider2D other)
        {
            Rune rune = other.GetComponent<Rune>();
            if (!cureValidChecking(rune)) { yield break; }
            EventManager.TriggerEvent(Planet.faction.StringType("AddCure"), -1);
            stage = 0;

            isCuring = true;
            Planet.visuals.Anim_Heal(INTERVAL);
            yield return new WaitForSeconds(5);
            isCuring = false;

            Planet.SetState(new WhiteDwarf(Planet));
        }
    }
}