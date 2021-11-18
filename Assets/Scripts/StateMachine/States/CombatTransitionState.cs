using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTransitionState : State
{
     public CombatTransitionState() : base() {
    }
    public override IEnumerator Start() {
        Debug.Log("starting Combat Transition state");
        yield return Execute();
    }
    public override IEnumerator End() {
        Debug.Log("Ending Combat Transition state");
        return base.End();
    }

    public override IEnumerator Execute() {
        Debug.Log("Executing Combat Transition state");
        return base.Execute();
    }

    public override IEnumerator Pause() {
        Debug.Log("pausing Combat Transition state");
        return base.Pause();
    }

    public override IEnumerator Resume() {
        Debug.Log("resuming Combat Transition state");
        return base.Resume();
    }
}
