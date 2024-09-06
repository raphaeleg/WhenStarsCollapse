using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sick : State
{
    public Sick(Planet planet) : base(planet) { }

    public int stage = 1;

    public override IEnumerator Start()
    {
        while (stage < 4)
        {
            yield return new WaitForSeconds(5);
            stage++;
            Debug.Log("Stage " + stage);
        }
        Planet.SetState(new BlackHole(Planet));
    }
}
