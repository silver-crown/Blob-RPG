﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// The netural state of the game world, where everything moves around in the overworld
/// </summary>
public partial class OverworldState : State
{
    public OverworldState() : base() {
    }
    /// <summary>
    /// Activate the grid movement system
    /// </summary>
    /// <returns></returns>
    public override IEnumerator Start() {
        Debug.Log("Starting PlayerWalking state");
        PlayerController.Player.GetComponent<Rigidbody2D>().useFullKinematicContacts = true;
        yield return Execute();
    }
    public override IEnumerator End() {
        ///<summary>stop executing the state</summary>
        Debug.Log("Ending PlayerWalking state");
        PlayerController.Player.GetComponent<PlayerGridMovement>().PauseAnimation();
        yield break;
    }

    public override IEnumerator Execute() {
        Debug.Log("Executing PlayerWalking state");
        ///<summary>This loop runs every frame until the key is pressed</summary>
        while (true) {
            ///<summary>Transitioning to the pause state via pause key</summary>
            if (Input.GetKeyDown(GameManager.GM.Pause)) {
                ///<summary>Wait for a frame after pressing the key</summary>
                yield return new WaitForEndOfFrame();
                ///<summary>End the state</summary>
                yield return End();
                ///<summary>Making sure there's an actual player controller to begin with</summary>
                if(PlayerController.Player.GetComponent<PlayerController>() != null) {
                    ///<summary>Set the state to PauseMenuState</summary>
                    GameManager.GM.SetState(new PauseMenuState());
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
                if (PlayerController.Player.GetComponent<PlayerGridMovement>().CanIInteractWithThis()) {
                    Debug.Log("It's an interactable object!");
                    ///<summary>End the state</summary>
                    yield return End();
                    ///<summary>Set the interaction state</summary>
                    GameManager.GM.SetState(new InteractState(PlayerController.Player.GetComponent<PlayerGridMovement>().CanIInteractWithThis()));
                    ///<summary>Break the iteration</summary>
                    yield break;
                }
                else {
                    Debug.Log("It's not an interactable object...");
                }
                ///<summary>Check if the point in front of the player is a valid object</summary>
            }
            ///<summary>Transitioning into a different area</summary>
            if (TransitionManager.TM.Transitioning) {
                yield return End();
                GameManager.GM.SetState(new TransitionState());
                yield break;
            }
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


