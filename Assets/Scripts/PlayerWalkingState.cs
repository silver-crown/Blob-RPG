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
        return Execute();
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
            if (Input.GetKeyDown(GameManager.GM.Test)) {
                ///<summary>End the state</summary>
                yield return End();
                ///<summary>Set the state to PauseMenuState</summary>
                GameManager.GM.SetState(new PauseMenuState(Player));
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
