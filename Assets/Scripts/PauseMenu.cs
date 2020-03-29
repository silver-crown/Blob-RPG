using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : State
{
    GameObject Player;
    public PauseMenu(GameObject player) {
        Player = player;
    }

    public override IEnumerator Start() {
        Debug.Log("Starting PauseMenu state");
        Execute();
        return base.Start();
    }
    public override IEnumerator End() {
        Debug.Log("Ending PauseMenu state");
        GameManager.GM.SetState(new PlayerWalking(Player));
        return base.End();
    }

    public override IEnumerator Execute() {
        Debug.Log("Executing PauseMenu state");
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


