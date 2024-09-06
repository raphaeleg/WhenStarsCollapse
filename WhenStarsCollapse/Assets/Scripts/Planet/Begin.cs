using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : State
{
    private const int VISIBILITY_DELAY = 3;

    public Begin(Planet planet) : base(planet) { }
    public override IEnumerator Start()
    {
        yield return new WaitForSeconds(VISIBILITY_DELAY);

        EventManager.TriggerEvent("isSuccessfulSpawn", 0);
        Planet.visuals.gameObject.SetActive(true);
        yield return new WaitForSeconds(VISIBILITY_DELAY);
        Planet.visuals.SetPlanetType((int) Planet.type);
        Planet.SetState(new Sick(Planet));
    }

    public override IEnumerator Shrink()
    {
        EventManager.TriggerEvent("isUnsuccessfulSpawn", 0);
        return base.Shrink();
    }
}
