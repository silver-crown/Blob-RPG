using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CombatResultScreen : MonoBehaviour
{
    public List<GameObject> playerNames = new List<GameObject>();
    public List<GameObject> playerEXP = new List<GameObject>();
    public List<GameObject> playerTempEXP = new List<GameObject>();

    void Update()
    {
        DisplayEXP();
    }

    void DisplayNames(){

    }
    void DisplayEXP(){
        int i = 0;
        int finishedDistributing = 0;
        int characterCount = 0;
        //need something that checks the total exp being given out 
        //partymanager's partycharacters each have a mychar component, this component contains the variables that need to be displayed
        foreach(GameObject p in PartyManager.PM.partyCharacters)
        {
            characterCount++;
            int k = p.GetComponent<player2DController>().myChar.expToBeGained;
            playerNames[i].GetComponent<TextMeshProUGUI>().SetText(p.GetComponent<player2DController>().myChar.pName.ToString());
            playerEXP[i].GetComponent<TextMeshProUGUI>().SetText(p.GetComponent<player2DController>().myChar.experiencePoints.ToString());
            playerTempEXP[i].GetComponent<TextMeshProUGUI>().SetText(k.ToString());
            //if the exp is done counting down, pressing a button should send the player back to the overworld
            if(k <= 0){    
                finishedDistributing++;    
            } 
            i++;
        }
        if (finishedDistributing == characterCount){
            if (Input.GetKeyDown(GameManager.GM.Attack)){
                StartCoroutine(TransitionManager.TM.TransitionIntoOverworldFromBattle());
            
            }
        }
    }
}
