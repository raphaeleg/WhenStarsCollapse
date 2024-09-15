using System.Collections;
using UnityEngine;
using Runes;

namespace Planets
{
    /// <summary>
    /// Handles a Sick Planet object behaviour.
    /// </summary>
    public class Sick : State
    {
        public Sick(Planet planet) : base(planet) { }

        public int stage = 1;
        public bool isCuring = false;
        private const int INTERVAL = 5;

        public override IEnumerator Start()
        {
            yield return new WaitForSeconds(INTERVAL);

            Planet.visuals.Anim_Sick();
            while (stage < 3 && stage > 0)
            {
                if (stage is 2) 
                { 
                    Planet.visuals.Anim_GetBig(); 
                }
                Planet.visuals.SickParticle(stage is 3);

                yield return new WaitForSeconds(INTERVAL);

                stage++;
            }

            Planet.visuals.SickParticle(false);
            if (stage >= 3) 
            { 
                Planet.SetState(new BlackHole(Planet)); 
            }
        }
        private bool CureValidChecking(Rune rune)
        {
            return rune && Planet.faction.CompareType(rune.faction) && !isCuring;
        }
        public override IEnumerator Collided(Collider2D other)
        {
            Rune rune = other.GetComponent<Rune>();
            if (!CureValidChecking(rune)) 
            { 
                yield break; 
            }

            EventManager.TriggerEvent(Planet.faction.StringType("AddCure"), -1);
            EventManager.TriggerEvent("AddCure", -1);

            isCuring = true;
            stage = 0;  // Automatically make planet entirely cured.
            Planet.visuals.Anim_Heal(INTERVAL);

            yield return new WaitForSeconds(5);

            isCuring = false;
            Planet.SetState(new WhiteDwarf(Planet));
        }
    }
}