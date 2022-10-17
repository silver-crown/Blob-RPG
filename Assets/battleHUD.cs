using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class battleHUD : MonoBehaviour
{
    public List<GameObject> playerHealths = new List<GameObject>();
    
    void Update()
    {
        DisplayPlayerHealth();
    }
    void DisplayPlayerHealth(){
        int i = 0;
        foreach(GameObject partyMem in CombatManager.CM.playerChars){
        playerHealths[i].GetComponent<TextMeshProUGUI>().SetText(partyMem.GetComponent<player2DController>().GetHP().ToString());
        i++;
        }
    }

    void DisplayCharPortraits(){
        //Display the correct portrait for the characters next to the health
    }
}


