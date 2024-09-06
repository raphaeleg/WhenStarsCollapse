using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : StateMachine
{
    public void Start()
    {
        SetState(new Begin(this));
    }
    public void Update()
    {
        StartCoroutine(State.Update());
    }
}
