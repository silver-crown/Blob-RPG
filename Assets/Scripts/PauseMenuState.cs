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
        return base.End();
    }

    public override IEnumerator Execute() {
        Debug.Log("Executing PauseMenu state");
        ///<summary>This loop runs every frame until the test key is pressed</summary>
        while (true) {
            if (Input.GetKeyDown(GameManager.GM.Test)) {
                ///<summary>Wait for a frame after pressing the key</summary>
                yield return new WaitForEndOfFrame();
                ///<summary>End the current state</summary>
                yield return End();
                ///<summary>Set the new state</summary>
                GameManager.GM.SetState(new PlayerWalkingState(Player));
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


