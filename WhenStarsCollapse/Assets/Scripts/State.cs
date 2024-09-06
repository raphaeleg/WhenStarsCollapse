using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class State
{
    protected Planet Planet;
    public State(Planet planet)
    {
        Planet = planet;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator Update()
    {
        yield break;
    }
    public virtual IEnumerator Heal()
    {
        yield break;
    }

    public virtual void OnTriggerStay2D(Collider2D other)
    {
        return;
    }

    public virtual IEnumerator Shrink()
    {
        Transform t = Planet.gameObject.transform;
        while (t.localScale.x > 0f)
        {
            t.localScale -= new Vector3(0.1f, 0.1f, 0f);
            yield return new WaitForSeconds(0.1f);
        }
        Planet.OnDestroy();
    }
}
