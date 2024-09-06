using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : State
{
    public Begin(Planet planet) : base(planet) { }
    public override IEnumerator Start()
    {
        yield return new WaitForSeconds(4);

        //SuccessfulSpawn.Raise();
        //highScore.stars++;

        Planet.SetState(new Sick(Planet));
    }

    public override IEnumerator Shrink()
    {
        // KillSpawnedPlanet.Raise();
        return base.Shrink();
    }
}
