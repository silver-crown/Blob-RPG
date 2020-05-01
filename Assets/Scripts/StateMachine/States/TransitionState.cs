
using System.Collections;
using UnityEngine;

public class TransitionState : State
{
    GameObject Player;
    GameObject Other;
    TransitionState(GameObject player, GameObject other) : base() {
        Player = player;
        Other = other;
    }

    public override IEnumerator Start() {
        return base.Start();
    }

    public override IEnumerator Execute() {
        return base.Execute();
    }

    public override IEnumerator End() {
        return base.End();
    }
    public override IEnumerator Pause() {
        return base.Pause();
    }

    public override IEnumerator Resume() {
        return base.Resume();
    }

}
