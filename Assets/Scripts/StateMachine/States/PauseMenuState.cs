using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuState : State
{
    MenuScript PauseMenu;
    public PauseMenuState(MenuScript pauseMenu) {
        PauseMenu = pauseMenu; 
    }

        public override IEnumerator Start() {
            Debug.Log("Starting PauseMenu state");
            PauseMenu.gameObject.SetActive(true);
            PauseMenu.transform.GetChild(0).gameObject.SetActive(true);
            GameManager.GM.FreezeAllEntities("Player", true);
            GameManager.GM.FreezeAllEntities("Enemy", true);
            Time.timeScale = 0;
            yield return Execute();
        }

        public override IEnumerator End() {
            Debug.Log("Ending PauseMenu state");
            PauseMenu.gameObject.SetActive(false);
            PauseMenu.transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log(PauseMenu.transform.GetChild(0).name);
            GameManager.GM.FreezeAllEntities("Player", false);
            GameManager.GM.FreezeAllEntities("Enemy", false);
            GameManager.GM.SetState(new OverworldState());
            Time.timeScale = 1;
            yield break;
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


