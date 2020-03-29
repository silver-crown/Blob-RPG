using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State State;
    /// <summary>
    /// Set the state
    /// </summary>
    /// <param name="state"></param>
    public void SetState(State state) {
        State = state;
        StartCoroutine(State.Start());
    }
}
