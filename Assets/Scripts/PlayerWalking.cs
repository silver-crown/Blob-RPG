using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class PlayerWalking : State
{

    private GameObject Player;
    public PlayerWalking(GameObject player) : base() {
        Player = player;
    }
    /// <summary>
    /// Activate the grid movement system
    /// </summary>
    /// <returns></returns>
    public override IEnumerator Start() {
        Debug.Log("Starting PlayerWalking state");
        Player.GetComponent<PlayerGridMovement>().enabled = true;
        ///<summary>Start executing the state</summary>
        Execute();
        return base.Start();
    }
    public override IEnumerator End() {
        ///<summary>stop executing the state</summary>
        Debug.Log("Ending PlayerWalking state");
        Player.GetComponent<PlayerGridMovement>().enabled = false;
        GameManager.GM.SetState(new PauseMenu(Player));
        return base.Execute();
    }

    public override IEnumerator Execute() {
        Debug.Log("Executing PlayerWalking state");
        if (Input.GetKey(GameManager.GM.Test)) {
            End();
        }
        return base.Execute();
    }

    public override IEnumerator Pause() {
        return base.Pause();
    }

    public override IEnumerator Resume() {
        return base.Resume();
    }
}
