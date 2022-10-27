using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public static PartyManager PM;
    public List<partyChar> playerCharacters = new List<partyChar>();
    public List<GameObject> partyCharacters = new List<GameObject>();

    private void Awake() {
        //If a manager doesn't already exist, make this the manager
        if (PM == null) {
            DontDestroyOnLoad(this);
            PM = this;
        }
        //if there is a manager 
        else if (PM != this) {
            Destroy(gameObject);
        }
        AddPlayerToActiveParty(playerCharacters[0]);
    }
    void Update(){

    }

    public void AddPlayerToActiveParty(partyChar p){
        partyCharacters.Add(p.character);
    }

}
