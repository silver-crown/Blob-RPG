using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CombatState : State
{
     public CombatState() : base() {
    }
    public override IEnumerator Start() {
        Debug.Log("starting Combat state");
        yield return new WaitForEndOfFrame();
        ///<summary>Iterate through all the currently loaded entities and enable them</summary>
        GameObject[] tag_1 = GameObject.FindGameObjectsWithTag("2DPlayer");  
 
        GameObject[] tag_2 = GameObject.FindGameObjectsWithTag("2DEnemy");  
 
        GameObject[] final_array = tag_1.Concat(tag_2).ToArray();

        foreach (var obj in final_array) {
            Behaviour[] behaviour = obj.GetComponents<Behaviour>();
                for(int i = 0; i < behaviour.Length; i++) {
                    behaviour[i].enabled = true;
                }
            if (obj.GetComponent<Rigidbody2D>()){
                obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        yield return Execute();
    }
    public override IEnumerator End() {
        Debug.Log("Ending Combat state");

        yield break;
    }
    
    public override IEnumerator Execute() {
        Debug.Log("Executing Combat state");
        while(true){
            if (Input.GetKeyDown(GameManager.GM.Pause)){
                Debug.Log("Pressed the pause key in combat");
                ///<summary>Wait for a frame after pressing the key</summary>
                yield return new WaitForEndOfFrame();
                ///<summary>End the current state</summary>
                ///<summary>Iterate through all the currently loaded entities and disable them</summary>
                 Time.timeScale = 0;
                GameObject[] tag_1 = GameObject.FindGameObjectsWithTag("2DPlayer");  
        
                GameObject[] tag_2 = GameObject.FindGameObjectsWithTag("2DEnemy");  
        
                GameObject[] final_array = tag_1.Concat(tag_2).ToArray();

                foreach (var obj in final_array) {
                    Behaviour[] behaviour = obj.GetComponents<Behaviour>();
                        for(int i = 0; i < behaviour.Length; i++) {
                            behaviour[i].enabled = false;
                        }
                    /* if (obj.GetComponent<Rigidbody2D>()){
                        obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                    }*/
                }
                GameManager.GM.SetState(new CombatPauseState(player2DController.Player.GetComponent<player2DController>().PauseMenu));
                yield break;
            }
            ///<summary>Wait for a frame</summary>
            yield return true;
        }
    }
}
