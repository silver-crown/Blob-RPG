using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : State
{
     public CombatState() : base() {
    }
    public override IEnumerator Start() {
        Debug.Log("starting Combat state");
        yield return Execute();
    }
    public override IEnumerator End() {
        Debug.Log("Ending Combat state");
        return base.End();
    }

    public override IEnumerator Execute() {
        Debug.Log("Executing Combat state");
        return base.Execute();
    }

    public override IEnumerator Pause() {
        Debug.Log("pausing Combat state");
        return base.Pause();
    }

    public override IEnumerator Resume() {
        Debug.Log("resuming Combat state");
        return base.Resume();
    }
}
