using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CombatPauseState : State
    {
        CombatPauseScript PauseMenu;
    
        public CombatPauseState(CombatPauseScript pauseMenu) : base() {
            PauseMenu = pauseMenu; 
        }
        public override IEnumerator Start() {
            Debug.Log("Starting combat pause state");
            GameManager.GM.FreezeAllEntities("2DPlayer", true);
            GameManager.GM.FreezeAllEntities("2DEnemy", true);
            PauseMenu.gameObject.SetActive(true);
            for(int i = 0; i < PauseMenu.transform.childCount; i++) {
                PauseMenu.transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return Execute();
        }
        public override IEnumerator End() {
            Debug.Log("Ending Combat pause state");
            PauseMenu.gameObject.SetActive(false);
            for(int i = 0; i < PauseMenu.transform.childCount; i++) {
                PauseMenu.transform.GetChild(i).gameObject.SetActive(false);
            }
            GameManager.GM.FreezeAllEntities("2DPlayer", false);
            GameManager.GM.FreezeAllEntities("2DEnemy", false);
            GameManager.GM.SetState(new CombatState());
            Time.timeScale = 1;
            yield break;
        }

        public override IEnumerator Execute() {
            Debug.Log("Executing combat pause state");
            while(true){
                if (Input.GetKeyDown(GameManager.GM.Pause)){
                    ///<summary>Wait for a frame after pressing the key</summary>
                    yield return new WaitForEndOfFrame();
                    ///<summary>End the current state</summary>
                    yield return End();
                    yield break;
                }
                //wait for a frame
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