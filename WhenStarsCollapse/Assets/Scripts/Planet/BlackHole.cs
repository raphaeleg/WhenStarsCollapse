using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : State
{
    public BlackHole(Planet planet) : base(planet) { }
    private const int RATE_OF_ROTATION = 10;

    public override IEnumerator Start()
    {
        Debug.Log("BlackHole");
        //StartCoroutine(ExplosionSFX());
        //BackgroundMusic.instance.PlaySFX("SFX_Wind_Loop");
        //highScore.blackHoles++;
        //animator.SetTrigger("Explode");
        //BlackholeAdded.Raise();
        yield break;
    }

    public override IEnumerator Update()
    {
        Planet.transform.Rotate(0, 0, Time.deltaTime * RATE_OF_ROTATION);
        yield break;
    }
}