
using System.Collections;
using UnityEngine;

class DialogueState : State
{
    GameObject Player;
    DialogueTrigger dialogueTrigger;
    public DialogueState(GameObject player, DialogueTrigger d) {
        Player = player;
        dialogueTrigger = d;
    }

    public override IEnumerator Start() {
        Debug.Log("Starting Dialogue State");
        return Execute();
    }
    public override IEnumerator End() {
        ///<summary>The new state is being set back to player in the previous state, nothing needs to be done here.</summary>
        GameManager.GM.SetState(new PlayerWalkingState(Player));
        return base.End();
    }

    public override IEnumerator Execute() {
        Debug.Log("Executing Dialogue state");
        dialogueTrigger.TriggerDialogue();
        ///<summary>This loop runs every frame until the test key is pressed</summary>
        while (true) {
            ///<summary>If there's no more sentences left to be displayed</summary>
            ///<summary>End the current state</summary>
          //  yield return End();
            ///<summary>Set the new state</summary>
            yield break;
        }
    }

    public override IEnumerator Pause() {
        return base.Pause();
    }

    public override IEnumerator Resume() {
        return base.Resume();
    }
}

