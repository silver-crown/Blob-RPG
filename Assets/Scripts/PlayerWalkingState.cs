using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class PlayerWalkingState : State
{
    private GameObject Player;
    private MenuScript PauseMenu;
    public PlayerWalkingState(GameObject player, MenuScript pauseMenu) : base() {
        Player = player;
        PauseMenu = pauseMenu;
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
            if (Input.GetKeyDown(GameManager.GM.Pause)) {
                ///<summary>Wait for a frame after pressing the key</summary>
                yield return new WaitForEndOfFrame();
                ///<summary>End the state</summary>
                yield return End();
                ///<summary>Set the state to PauseMenuState</summary>
                GameManager.GM.SetState(new PauseMenuState(Player,PauseMenu));
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
