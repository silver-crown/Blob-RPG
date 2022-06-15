
using System.Collections;
using UnityEngine;

class DialogueState : State
{
    Interactable interactable;
    DialogueTrigger dialogueTrigger;
    public DialogueState(Interactable i) {
        interactable = i;
        dialogueTrigger = i.GetComponent<DialogueTrigger>();
    }

    public override IEnumerator Start() {
        Debug.Log("Starting Dialogue State");
        DialogueManager.DM.dialogueBox.gameObject.SetActive(true);
        return Execute();
    }
    public override IEnumerator End() {
        ///<summary>The new state is being set back to player in the previous state, nothing needs to be done here.</summary>
        yield break;
    }

    public override IEnumerator Execute() {
        Debug.Log("Executing Dialogue state");
        dialogueTrigger.TriggerDialogue();
        ///<summary>This loop runs every frame until the test key is pressed</summary>
        while (true) {
            ///<summary>Wait for a frame</summary>
            yield return new WaitForEndOfFrame();
            ///<summary>If there's no more dialogues left to be displayed</summary>
            if (DialogueManager.DM.done) {
                    DialogueManager.DM.dialogueBox.gameObject.SetActive(false);
                switch (dialogueTrigger.transitioningState) {
                    case DialogueTrigger.TransitioningState.WalkingState:
                        interactable.InteractWith();
                        GameManager.GM.SetState(new OverworldState());
                        break;
                    case DialogueTrigger.TransitioningState.BattleState:
                        break;
                    case DialogueTrigger.TransitioningState.ShopState:
                        break;
                }
                ///<summary>Reset DM's done so new dialogues can be initiated some time in the future</summary>
                DialogueManager.DM.done = false;
                yield return End();
            }
            ///<summary>End the current state</summary>
          //  yield return End();
            ///<summary>Set the new state</summary>
            yield return true;
        }
    }
}

