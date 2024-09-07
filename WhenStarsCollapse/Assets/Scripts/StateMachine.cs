using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State State;

        public void SetState(State state)
        {
            State = state;
            StartCoroutine(State.Start());
        }
    }
}