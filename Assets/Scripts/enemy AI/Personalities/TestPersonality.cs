using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestPersonality : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        GetComponent<Prototype01>().MakeTestUtility("moveCloser", "normalBehavior");
        GetComponent<Prototype01>().MakeTestUtility("meleeAttack", "normalBehavior");
        GetComponent<Prototype01>().MakeTestUtility("runAway", "normalBehavior");
        GetComponent<Prototype01>().MakeTestUtility("heal", "normalBehavior");
        GetComponent<Prototype01>().MakeTestUtility("foobar", "notSoNormalBehavior");
        GetComponent<Prototype01>().MakeTestUtility("snafu", "test category 3");
    }
    void Update(){
        //calculate utility for toileting pants
        GetComponent<Prototype01>().AssignCategoryValue("normalBehavior", 42);
        //GetComponent<Prototype01>().AssignCategoryValue("notSoNormalBehavior", 100);
        DistanceFromPlayer();
    }

    #region Utility factors
    void DistanceFromPlayer(){
        int maxValue = 100;
        //a calculation should happen here    
        //find the closest player character
        player2DController[] Playerchars = (player2DController[]) GameObject.FindObjectsOfType(typeof(player2DController)); ;
        player2DController closestPlayer = null;
        float distanceToClosestPlayer;
        for (int i = 0; i < Playerchars.Length; i++){
            //set closestplayer to be the partycharacter with the smallest distance to me
            if(i == 0){
                closestPlayer = Playerchars[i];
                distanceToClosestPlayer = Vector3.Distance(this.transform.position, closestPlayer.transform.position);
            }
            else{
                //if the distance between the partycharacter and me is greater than closestPlayer, overwrite closestPlayer
                distanceToClosestPlayer = Vector3.Distance(this.transform.position, closestPlayer.transform.position);
                float distanceToPartyCharacter = Vector3.Distance(this.transform.position, Playerchars[i].transform.position);
                if (distanceToClosestPlayer > distanceToPartyCharacter){
                    closestPlayer = Playerchars[i];
                }
            }
        }
        //find the distance between the closest player character and me
        distanceToClosestPlayer = Vector3.Distance(this.transform.position, closestPlayer.transform.position);
        //Debug.Log("distance to player:" + distanceToClosestPlayer);
        //X is a normalized value that decreases when the player is close (player's distance from me)
        double x = ((double)distanceToClosestPlayer / (double)maxValue);
        Debug.Log("x value = " + x);
        GetComponent<Prototype01>().AssignUtilityValue("moveCloser", x);
    }
    void IAmLowOnHealth(){
        float dangerThreshold = 0.1f;
        //is my health 10% or lower
        if(GetComponent<Enemy>().getHP() <= GetComponent<Enemy>().getMaxHP() * dangerThreshold){
            //calculation magic voodo mumbo jumbo
            //return a normalized value
        }
    }

    void InRangeForMeleeAttack(){

    }

    #endregion

    #region Utility actions
   // double GetCloserToPlayer(){
        //needs the distance from the player, it should also see if it's in danger aka has low healthies
        //it should normalize the values obtained from the data to a number between 0 and 100

        //i = ((double)distancefromplayer + IAmLowOnHealth /(double)max)*100;
        //it should then normalize THIS value to a number between 0 and 1, and return this value for the final decision on which utility action to perform
        // return Math.Pow(((double)i/(double)max),k);
   // }
    void AttackPlayer(){

    }
    void RunAwayFromPlayer(){

    }
    #endregion
}
