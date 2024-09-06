using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sick : State
{
    public Sick(Planet planet) : base(planet) { }

    public int stage = 1;
    public bool isCuring = false;
    

    public override IEnumerator Start()
    {
        // GetSick() Animation
        while (stage < 4 && stage > 0)
        {
            yield return new WaitForSeconds(5);
            stage++;
            Debug.Log("Stage " + stage);
            // stage == 2 && GetBig() Animation
            //SickParticles(stage == 3)
        }
        Planet.SetState(stage == 4 ? new BlackHole(Planet) : new WhiteDwarf(Planet));
    }
    public override IEnumerator Heal()
    {
        if (isCuring) { yield break; }

        isCuring = true;
        //CuringParticles.SetActive(true);

        yield return new WaitForSeconds(5);

        isCuring = false;
        //CuringParticles.SetActive(false);

        Planet.SetState(new WhiteDwarf(Planet));    // TODO
    }
    
}
