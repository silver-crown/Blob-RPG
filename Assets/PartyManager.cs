using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public static PartyManager PM;
    public List<partyChar> playerCharacters = new List<partyChar>();
    public List<GameObject> partyCharacters = new List<GameObject>();

    [SerializeField] public bool addingEXPToChars;

    private float interval = 0.05f;
    private float timer = 0.0f;

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
         AddPlayerToActiveParty(playerCharacters[1]);
    }
    void Update(){
        if(addingEXPToChars){
            timer += Time.deltaTime;
            if(timer >= interval){
                AddTempEXPToCharacters();
                timer = 0.0f;
            }
        }
    }

    public void AddPlayerToActiveParty(partyChar p){
        SetUpCharStats(p);
        partyCharacters.Add(p.character);
    }

    public void SetUpCharStats(partyChar p){
        p.character.GetComponent<player2DController>().SetUpCharStats(p);
    }

    //*****bugged function, fix in the future.
    public void AddTempEXPToCharacters(){
        foreach (partyChar p in playerCharacters){
            //display exp and the exp to be gained
            //add the temp exp to the exp 1 digit at a time until temp reaches 0, then stop displaying temp

            //for loop is too fast here...
            //need to use time.deltatime in update
            if(p.expToBeGained > 0){
                p.experiencePoints ++;
                p.expToBeGained --;
            }
        }
    }
}