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
        private const int HEAL_DURATION = 5;

        public override IEnumerator Start()
        {
            Planet.visuals.Anim_Sick();
            while (stage < 4 && stage > 0)
            {
                yield return new WaitForSeconds(5);
                stage++;

                if (stage == 2) { Planet.visuals.Anim_GetBig(); }
                Planet.visuals.SickParticle(stage == 3);
            }
            Planet.SetState(stage == 4 ? new BlackHole(Planet) : new WhiteDwarf(Planet));
        }
        private bool cureValidChecking(Rune rune)
        {
            return rune && IsRightCure(rune.GetType()) && !isCuring;
        }
        private bool IsRightCure(string item) { return Planet.faction.type.ToString() == item; }

        public override IEnumerator Collided(Collider2D other)
        {
            Rune rune = other.GetComponent<Rune>();
            if (!cureValidChecking(rune)) { yield break; }

            EventManager.TriggerEvent(Planet.faction.StringType("AddCure"), -1);

            Planet.visuals.Anim_Heal(HEAL_DURATION);
            isCuring = true;
            yield return new WaitForSeconds(5);
            isCuring = false;
            Planet.SetState(new WhiteDwarf(Planet));
        }
    }
}