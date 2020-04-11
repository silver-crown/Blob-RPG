using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State for interacting with an object in front of the player, takes an object as an argument in the constructor parameter
/// </summary>
public class InteractState : State
{
    public GameObject Player;
    public GameObject other;
    private Interactable interactable;

    public InteractState(GameObject player,GameObject Other) : base() {
        Player = player;
        other = Other;
        interactable = Other.gameObject.GetComponent<Interactable>();
    }

    public override IEnumerator Start() {
        Debug.Log("Starting Interact State");
        yield return Execute();
    }

    public override IEnumerator End() {
        Debug.Log("Ending Interact State");
        return base.End();
    }

    public override IEnumerator Execute() {
        while (true) {
            Debug.Log("Executing Interact state");
            ///<summary>Wait for a frame</summary>
            yield return new WaitForEndOfFrame();

            //Get the interactable's dialoguetrigger
            if(interactable.GetComponent<DialogueTrigger>() != null) {
                yield return End();
                ///<summary>Start the dialogue state</summary>
                GameManager.GM.SetState(new DialogueState(Player ,interactable.GetComponent<DialogueTrigger>()));
            }
            interactable.InteractWith();
            yield return End();
            ///<summary>Iteration ends here.</summary>
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
