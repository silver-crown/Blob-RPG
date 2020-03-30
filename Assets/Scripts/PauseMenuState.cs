using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuState : State
{
    GameObject Player;
    public PauseMenuState(GameObject player) {
        Player = player;
    }

    public override IEnumerator Start() {
        Debug.Log("Starting PauseMenu state");
        return Execute();
    }
    public override IEnumerator End() {
        Debug.Log("Ending PauseMenu state");
        GameManager.GM.SetState(new PlayerWalkingState(Player));
        return base.End();
    }

    public override IEnumerator Execute() {
        Debug.Log("Executing PauseMenu state");
        ///<summary>This loop runs every frame until the test key is pressed</summary>
        while (true) {
            if (Input.GetKeyDown(GameManager.GM.Test)) {
                yield return End();
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


