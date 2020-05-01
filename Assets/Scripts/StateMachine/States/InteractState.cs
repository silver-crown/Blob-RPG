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
        return base.End();
    }

    public override IEnumerator Execute() {
        while (true) {
            Debug.Log("Executing Interact state");
            ///<summary>Wait for a frame</summary>
            yield return new WaitForEndOfFrame();

            //<summary>Get the interactable's dialoguetrigger</summary>
            if(interactable.GetComponent<DialogueTrigger>() != null) {
                ///<summary>Start the dialogue state</summary>
                GameManager.GM.SetState(new DialogueState(Player, interactable));
            }
            ///<summary>If there is none just interact normally</summary>
            else {
                interactable.InteractWith();
                ///<summary>Start the walking state</summary>
                GameManager.GM.SetState(new PlayerWalkingState(Player));
            }
            ///<summary>Iteration ends here.</summary>
            yield break;
        }
    }
}
