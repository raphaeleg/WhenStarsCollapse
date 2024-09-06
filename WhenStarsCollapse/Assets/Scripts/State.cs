using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
