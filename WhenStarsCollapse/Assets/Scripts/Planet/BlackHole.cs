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
        /*StartCoroutine(ExplosionSFX());
        BackgroundMusic.instance.PlaySFX("SFX_Wind_Loop");
        highScore.blackHoles++;
        animator.SetTrigger("Explode");
        BlackholeAdded.Raise();*/
        yield break;
    }

    public override IEnumerator Update()
    {
        Planet.transform.Rotate(0, 0, Time.deltaTime * RATE_OF_ROTATION);
        yield break;
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        /*var blackHoleEat = Instantiate(BlackholeOverlay);
        blackHoleEat.GetComponent<AnimationNoLoop>().destroySelf = true;
        blackHoleEat.transform.SetParent(transform);
        blackHoleEat.transform.localScale = new Vector3(2, 2, 2);
        blackHoleEat.transform.localPosition = Vector3.zero;*/

        GameObject collider = other.gameObject;
        Planet p = collider.GetComponent<Planet>();
        if (p != null)
        {
        //    if (p.state == PlanetStates.BLACKHOLE) { return; }
        //    p.ShrinkUntilDestroy();
        }
        //else { collider.GetComponent<TakeItem>().ShrinkUntilDestroy(); }
        //StartCoroutine(MoveCollider(collider));
    }

    public IEnumerator MoveCollider(GameObject collider)
    {
        float time = 0.5f;
        while (time > 0 && collider != null)
        {
            var directionToCollider = collider.transform.position - Planet.transform.position;
            collider.transform.localPosition -= 5f * directionToCollider;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override IEnumerator Shrink()
    {
        yield break;
    }
}