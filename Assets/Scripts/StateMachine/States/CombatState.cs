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
                GameManager.GM.SetState(new CombatPauseState(player2DController.Player.GetComponent<player2DController>().PauseMenu));
                yield break;
            }
            ///<summary>Wait for a frame</summary>
            yield return true;
        }
    }
}
