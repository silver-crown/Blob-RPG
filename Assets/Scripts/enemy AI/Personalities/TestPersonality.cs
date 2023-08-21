using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//This script should be attached to an entity, dictating what actions it prefers to make overall
//The idea is that there is a neutral personality, a cowardly personality, brave personality and so on...
//These will be attached to the entities at random during encounters, making each encounter unique.
public class TestPersonality : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        //make some dummy utilities to test the debug menu
        GetComponent<Prototype01>().MakeTestUtility("moveCloser", "normalBehavior", GetCloserToPlayer);
        GetComponent<Prototype01>().MakeTestUtility("meleeAttack", "normalBehavior", AttackPlayer);
        GetComponent<Prototype01>().MakeTestUtility("runAway", "normalBehavior", RunAwayFromPlayer);
        //GetComponent<Prototype01>().MakeTestUtility("heal", "normalBehavior");
       // GetComponent<Prototype01>().MakeTestUtility("foobar", "notSoNormalBehavior");
       //GetComponent<Prototype01>().MakeTestUtility("snafu", "test category 3");
    }
    void Update(){
        GetComponent<Prototype01>().AssignCategoryValue("normalBehavior", 42);
        //GetComponent<Prototype01>().AssignCategoryValue("notSoNormalBehavior", 100);
        Tuple<float, player2DController> i = DistanceFromPlayer();
        InRangeForMeleeAttack();
    }

    #region Utility factors
    Tuple<float, player2DController> DistanceFromPlayer(){
        int maxValue = 100;   
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
        float x = ((float)distanceToClosestPlayer / (float)maxValue);
        //Debug.Log("x value = " + x);
        GetComponent<Prototype01>().AssignUtilityValue("moveCloser", "normalBehavior", x);
        //returns the distance and the player
        return  Tuple.Create((float)distanceToClosestPlayer, closestPlayer);
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
        int maxValue = 100;
        float attackValue = 0;
        Tuple<float, player2DController> i = DistanceFromPlayer();
        //find the distance between the closest player character and me
        if(i.Item1 <= 1){
            attackValue = 4;
        }   
        //float x = ((float)distanceToClosestPlayer / (float)maxValue);
        GetComponent<Prototype01>().AssignUtilityValue("meleeAttack", "normalBehavior", (float)attackValue / (float)maxValue);
    }

    #endregion

    #region Utility actions
    //the only thing this method should do is get closer to the player, the calculations themselves are done in the factor functions
    public void GetCloserToPlayer(){
        Vector3 myPos = this.transform.position;
        float testMoveSpeed = 0.02f;
        //grab the player from the utility function 
        Tuple<float, player2DController> i = DistanceFromPlayer();
        if (i.Item2.transform.position.x < myPos.x - 0.6){
            this.transform.position -= new Vector3(testMoveSpeed, 0.0f, 0.0f);
        }
        else{
            this.transform.position += new Vector3(testMoveSpeed, 0.0f, 0.0f);
        }

    }
    void AttackPlayer(){
        Debug.Log("PERFORMING ATTACK");

        //debug log: starting attack animation
        //1, 2, 3
        //attack/spawn hitbox (???)
        /*
        if personality is unique to creature, all attack specifics need to happen here
        ************
        if personality is not unique to creature (can be applied to bat as well as a goblin) then all attack specifics will need to happen on the creature prefab 

         */
    }
    void RunAwayFromPlayer(){
        Debug.Log("RUNNING AWAY");
    }
    #endregion
}
