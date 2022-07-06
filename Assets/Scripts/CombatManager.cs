using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager CM;
    public bool Fighting;
    public Animator transition;
    [SerializeField] public GameObject blueDmgPopup;
    public enum Backdrops{
        Forest,
        Field,
        Water,
        Beach,
        Desert,
        Volcano,
        MountainTop,
        House,
        Castle,
        Town
    }
    public Backdrops backdrop;
    public Entity[] enemies;
    //the party
    static public GameObject Player;
    
    private void Awake() {
        //If a manager doesn't already exist, make this the manager
        if (CM == null) {
            DontDestroyOnLoad(this);
            CM = this;
        }
        //if there is a manager 
        else if (CM != this) {
            Destroy(gameObject);
        }
    }
    public IEnumerator CycleCharacters(){
        Debug.Log(GameManager.GM.partyChars[0]);
        Debug.Log(GameManager.GM.partyChars[1]);
        //get who's next in line and set them to be the player character, set self to be AI
        for( int i = 0; i <= GameManager.GM.partyChars.Length-1; i++){
            Debug.Log(i);
            //if the character is the player character
            if(GameManager.GM.partyChars[i].GetComponent<player2DController>().playerChar == true){
                //if there's no one next in line, get the one first in line
                if(i == GameManager.GM.partyChars.Length-1){
                    GameManager.GM.partyChars[0].GetComponent<player2DController>().playerChar = true;
                    GameManager.GM.partyChars[0].GetComponent<SpriteRenderer>().color = Color.clear;
                     yield return new WaitForSeconds(0.01f);
                    GameManager.GM.partyChars[0].GetComponent<SpriteRenderer>().color = Color.white;

                //else just change the one next in line
                } else{
                    GameManager.GM.partyChars[i+1].GetComponent<player2DController>().playerChar = true;
                    GameManager.GM.partyChars[i+1].GetComponent<SpriteRenderer>().color = Color.clear;
                    yield return new WaitForSeconds(0.01f);
                    GameManager.GM.partyChars[i+1].GetComponent<SpriteRenderer>().color = Color.white;
                }
                //set self to AI
                GameManager.GM.partyChars[i].GetComponent<player2DController>().playerChar = false;
                yield break;
            } 
        }
    }

}
