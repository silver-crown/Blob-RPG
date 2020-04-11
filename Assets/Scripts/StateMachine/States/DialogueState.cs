
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
        Debug.Log("Ending Dialogue State");
        ///<summary>The new state is being set back to player in the previous state, nothing needs to be done here.</summary>
        return base.End();
    }

    public override IEnumerator Execute() {
        Debug.Log("Executing Dialogue state");
        dialogueTrigger.TriggerDialogue();
        ///<summary>This loop runs every frame until the test key is pressed</summary>
        while (true) {
            ///<summary>Wait for a frame after pressing the key</summary>
            yield return new WaitForEndOfFrame();
            ///<summary>End the current state</summary>
            yield return End();
            ///<summary>Set the new state</summary>
            GameManager.GM.SetState(new PlayerWalkingState(Player));
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

