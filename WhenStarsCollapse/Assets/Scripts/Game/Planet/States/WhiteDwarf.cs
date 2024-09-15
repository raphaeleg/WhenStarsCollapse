using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Planets
{
    /// <summary>
    /// Handles a White Dwarf object behaviour.
    /// </summary>
    public class WhiteDwarf : State
    {
        public WhiteDwarf(Planet planet) : base(planet) { }

        public int stage = 1;
        private const int INTERVAL = 5;
        private const int ANIM_DESTROY_TIME = 1;

        public override IEnumerator Start()
        {
            Planet.visuals.Anim_BecomeDwarf();
            EventManager.TriggerEvent("whiteDwarfSpawn", 0);

            yield return new WaitForSeconds(INTERVAL);

            Planet.visuals.Anim_DestroyDwarf();
            yield return new WaitForSeconds(ANIM_DESTROY_TIME);
            Planet.OnDestroy();
        }
    }
}