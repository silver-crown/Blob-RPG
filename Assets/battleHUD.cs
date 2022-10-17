using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class battleHUD : MonoBehaviour
{
    public List<GameObject> playerHealths = new List<GameObject>();
    
    void Update()
    {
        int i = 0;
        foreach(GameObject partyMem in GameManager.GM.partyChars){
        playerHealths[i].GetComponent<TextMeshProUGUI>().SetText(partyMem.GetComponent<player2DController>().GetHP().ToString());
        i++;
        }

    }
}
