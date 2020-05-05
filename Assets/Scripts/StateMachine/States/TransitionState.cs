
using System.Collections;
using UnityEngine;

public class TransitionState : State
{
    public TransitionState() : base() {
    }

    public override IEnumerator Start() {
        yield return Execute();
    }

    public override IEnumerator Execute() {
        while (true) {

            ///<summary>If it's done transitioning, the world can move again</summary>
            if (!TransitionManager.TM.Transitioning) {
                GameManager.GM.SetState(new OverworldState());
                yield break;
            }

            ///<summary>TODO: Transition into a battle state when this is appropriate.</summary>
            yield return true;
        }
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
