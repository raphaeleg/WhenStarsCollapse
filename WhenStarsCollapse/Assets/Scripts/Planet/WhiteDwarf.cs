using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WhiteDwarf : State
{
    public WhiteDwarf(Planet planet) : base(planet) { }

    public int stage = 1;

    public override IEnumerator Start()
    {
        Planet.visuals.Anim_BecomeDwarf();
        EventManager.TriggerEvent("whiteDwarfSpawn", 0);

        yield return new WaitForSeconds(20);
        
        Planet.visuals.Anim_DestroyDwarf();
        yield return new WaitForSeconds(0.5f);
        Planet.OnDestroy();
    }
}
