using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            // GetSick() Animation
            while (stage < 4 && stage > 0)
            {
                yield return new WaitForSeconds(5);
                stage++;
                Debug.Log("Stage " + stage);

                if (stage == 2) { Planet.visuals.Anim_GetBig(); }
                Planet.visuals.SickParticle(stage == 3);
            }
            Planet.SetState(stage == 4 ? new BlackHole(Planet) : new WhiteDwarf(Planet));
        }
        public override IEnumerator Heal()
        {
            if (isCuring) { yield break; }
            Planet.visuals.Anim_Heal(HEAL_DURATION);
            isCuring = true;
            yield return new WaitForSeconds(5);
            isCuring = false;
            Planet.SetState(new WhiteDwarf(Planet));    // TODO
        }
        
    }
}