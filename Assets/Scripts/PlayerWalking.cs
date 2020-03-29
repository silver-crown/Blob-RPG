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
        return base.Start();
    }
    public override IEnumerator End() {
        return base.End();
    }

    public override IEnumerator Execute() {
        return base.Execute();
    }

    public override IEnumerator Pause() {
        return base.Pause();
    }

    public override IEnumerator Resume() {
        return base.Resume();
    }

}
