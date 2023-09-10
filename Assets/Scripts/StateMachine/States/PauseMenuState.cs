using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuState : State
{
        public override IEnumerator Start() {
            Debug.Log("Starting PauseMenu state");
            Object.Instantiate(Resources.Load("OverworldPauseMenu", typeof(GameObject)));
            GameManager.GM.FreezeAllEntities("Player", true);
            GameManager.GM.FreezeAllEntities("Enemy", true);
            Time.timeScale = 0;
            yield return Execute();
        }

        public override IEnumerator End() {
            Debug.Log("Ending PauseMenu state");
            GameManager.GM.FreezeAllEntities("Player", false);
            GameManager.GM.FreezeAllEntities("Enemy", false);
            Time.timeScale = 1;
            yield return new WaitForEndOfFrame();
            GameManager.GM.SetState(new OverworldState());
            yield break;
        }

    public override IEnumerator Execute() {
        Debug.Log("Executing PauseMenu state");
        ///<summary>This loop runs every frame until the test key is pressed</summary>
        while (true) {
            /*If the player presses the pause button while in the pause menu state, all menus should be
            destroyed, and the player should be taken out of the pause state and back into the overworld*/
            if (Input.GetKeyDown(GameManager.GM.Pause) ) {
                GameMenu[] allMenus = UnityEngine.Object.FindObjectsOfType<GameMenu>();
                foreach(GameMenu menu in allMenus) {
                    Object.Destroy(menu.gameObject);
                }
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
}


