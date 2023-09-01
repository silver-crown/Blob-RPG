
using System.Collections;
using UnityEngine;

public class TransitionState : State
{
    public TransitionState() : base() {
    }

    public override IEnumerator Start() {
        Debug.Log("Starting Black Transition state");
        yield return Execute();
    }

    public override IEnumerator Execute() {
        while (true) {
            switch(TransitionManager.TM.transitionType){
                ///<summary>if it's a normal transition</summary>
                case(TransitionManager.TransitionType.Normal):
                    ///<summary>If it's done transitioning, the world can move again</summary>
                    if (!TransitionManager.TM.Transitioning) {
                        GameManager.GM.SetState(new OverworldState());
                        yield break;
                    }   
                    break;
                ///<summary>if it's a combat encounter</summary>
                case(TransitionManager.TransitionType.Enemy):
                    if(!TransitionManager.TM.Transitioning){
                        GameManager.GM.SetState(new CombatState());
                        yield break;
                    }
                    break;
                case(TransitionManager.TransitionType.Boss):
                    break;
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
