using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class PlayerWalkingState : State
{
    private GameObject Player;
    public PlayerWalkingState(GameObject player) : base() {
        Player = player;
    }
    /// <summary>
    /// Activate the grid movement system
    /// </summary>
    /// <returns></returns>
    public override IEnumerator Start() {
        Debug.Log("Starting PlayerWalking state");
        Player.GetComponent<PlayerGridMovement>().enabled = true;
        yield return Execute();
    }
    public override IEnumerator End() {
        ///<summary>stop executing the state</summary>
        Debug.Log("Ending PlayerWalking state");
        Player.GetComponent<PlayerGridMovement>().PauseAnimation();
        Player.GetComponent<PlayerGridMovement>().enabled = false;
        return base.End();
    }

    public override IEnumerator Execute() {
        Debug.Log("Executing PlayerWalking state");
        ///<summary>This loop runs every frame until the test key is pressed</summary>
        while (true) {
            ///<summary>Transitioning to the pause state via pause key</summary>
            if (Input.GetKeyDown(GameManager.GM.Pause)) {
                ///<summary>Wait for a frame after pressing the key</summary>
                yield return new WaitForEndOfFrame();
                ///<summary>End the state</summary>
                yield return End();
                ///<summary>Making sure there's an actual player controller to begin with</summary>
                if(Player.GetComponent<PlayerController>() != null) {
                    ///<summary>Set the state to PauseMenuState</summary>
                    GameManager.GM.SetState(new PauseMenuState(Player, Player.GetComponent<PlayerController>().PauseMenu));
                }
                ///<summary>Iteration ends here.</summary>
                yield break;
            }
            ///<summary>Interacting with an object with the interact key</summary>
            if (Input.GetKeyDown(GameManager.GM.Interact)) {
                ///<summary>Wait for a frame after pressing the key</summary>
                yield return new WaitForEndOfFrame();
                Debug.Log("Pressed the Interact key");
                ///<summary>check if the object is interactable</summary>
                if (Player.GetComponent<PlayerGridMovement>().CanIInteractWithThis()) {
                    Debug.Log("It's an interactable object!");
                    ///<summary>End the state</summary>
                    yield return End();
                    ///<summary>Set the interaction state</summary>
                    GameManager.GM.SetState(new InteractState(Player, Player.GetComponent<PlayerGridMovement>().CanIInteractWithThis()));
                    ///<summary>Break the iteration</summary>
                    yield break;
                }
                else {
                    Debug.Log("It's not an interactable object...");
                }
                ///<summary>Check if the point in front of the player is a valid object</summary>
            }
            ///<summary>Transitioning</summary>

            ///<summary>Wait for a frame</summary>
            yield return true;
        }
    }

    public override IEnumerator Pause() {
        return base.Pause();
    }

    public override IEnumerator Resume() {
        return base.Resume();
    }
}


