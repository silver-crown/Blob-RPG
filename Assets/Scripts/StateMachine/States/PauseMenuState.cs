using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuState : State
{
    GameObject Player;
    MenuScript PauseMenu;
    public PauseMenuState(GameObject player, MenuScript pauseMenu) {
        Player = player;
        PauseMenu = pauseMenu; 
    }

    public override IEnumerator Start() {
        Debug.Log("Starting PauseMenu state");
        PauseMenu.gameObject.SetActive(true);
       for(int i = 0; i <= PauseMenu.transform.childCount - 1; i++) {
        PauseMenu.transform.GetChild(0).gameObject.SetActive(true);
        }
        return Execute();
    }
    public override IEnumerator End() {
        Debug.Log("Ending PauseMenu state");
        PauseMenu.gameObject.SetActive(false);
        for (int i = 0; i <= PauseMenu.transform.childCount - 1; i++) {
        PauseMenu.transform.GetChild(0).gameObject.SetActive(false);
        }
        return base.End();
    }

    public override IEnumerator Execute() {
        Debug.Log("Executing PauseMenu state");
        ///<summary>This loop runs every frame until the test key is pressed</summary>
        while (true) {
            if (Input.GetKeyDown(GameManager.GM.Pause)) {
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


