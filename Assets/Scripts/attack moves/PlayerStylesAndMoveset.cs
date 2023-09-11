using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;


//This script should handle all the logic surrounding the switching of player move sets (styles) 
//A single style is a list of moves the player can perform, mapped to each of the action buttons
public class PlayerStylesAndMoveset : MonoBehaviour
{
    private KeyCode BottomAction;
    private KeyCode TopAction; 
    private KeyCode LeftAction; 
    private KeyCode RightAction;

    int selectedStyleIteration = 0;

    //List of all moves available to the player, unlocked or otherwise. Should be ordered by the level they're unlocked
    public List<PlayerMove> Moves = new List<PlayerMove>();
    //A single style is a list of moves the player can perform, mapped to each of the action buttons


    //The selected style
    private PlayerMove[] SelectedStyle = new PlayerMove[4];
    //List of individual styles, customized and created by the player
    private List<PlayerMove[]> StylesList = new List<PlayerMove[]>();

    private void Start() {
        BottomAction = GameManager.GM.BottomAttack;
        TopAction = GameManager.GM.TopAttack;
        LeftAction  = GameManager.GM.LeftAttack;
        RightAction = GameManager.GM.RightAttack;
        SetUpMovesList();
        SetUpStarterStyle();
    }

    /*Fills the moves list, should be ordered by the level they're unlocked
     **Should run ONCE at startup. */
    public void SetUpMovesList() {
        PlayerMove[] allAvailableMoves = GameObject.FindObjectsOfType<PlayerMove>();
        foreach (PlayerMove move in allAvailableMoves) {
            Moves.Add(move);
        }
        Moves.OrderBy(o=>o.unlocksAtLevel);
    }


    /*Should get the move that unlock at level 1, and insert these into the StylesList.
    + SelectedStyle to be the starter style*/
    public void SetUpStarterStyle() {
        //A single style is an array of moves the player can perform, mapped to each of the action buttons
        PlayerMove[] style = new PlayerMove[4];
        foreach(PlayerMove move in Moves) { 
            //get the move that unlock at level 1
            if (move.unlocksAtLevel == 1) {
                //fill all the slots in the new Style with the move
                for (int i = 0; i <= 3; i++) {
                    style[i] = move;
                }
                break;
            }
        }
        SelectedStyle = style;
        //put the style in StylesList
        //set selectedStyle to this list
    }

    /*Switches the player's currently active style.
     * Should run freely when the player presses the cycle buttons in COMBAT state */
    public void SwitchStylesUponInput() {
        //press the cycle left key
        if (Input.GetKey(GameManager.GM.CycleLeft)) {
            ++selectedStyleIteration;
            if(selectedStyleIteration >= StylesList.Count) {
                //if you've exceeded the number of styles
                selectedStyleIteration = 1;
                SelectedStyle = StylesList[selectedStyleIteration-1];
                return;
            }
            SelectedStyle = StylesList[selectedStyleIteration-1];

            //should NOT be able to switch to style if the style is empty
        }

        //SelectedStyle's content should be displayed in the HUD, but not by this file
    }


    /*Self explanatory
     Should run in the pause state when the player presses the ADD button*/
    public void AddStyle(PlayerMove[] style) {
        StylesList.Add(style);
    }

    /*Self explanatory
     Should run in the pause state when the player presses the DELETE button*/
    public void DeleteStyle(int styleIndex) {
        StylesList.RemoveAt(styleIndex);
        
    }
    //should handle the logic for displaying the currently selected style in the 2D combat hud
    public void DisplaySelectedStyle(int styleIndex) {
        return;
    }

    /*This method would take the original move list from the style (grabbed from the menu) and rewrite it to the edited list
     this method should only run once the player presses the confirm button, it will then take the new list of moves and pass it
     to the EditStyle method as a parameter*/
    public void EditStyle(int originalIndex, PlayerMove[] edited) {
        StylesList[originalIndex] = edited;
    }

    //Method should handle logic related to pressing the different action buttons. 
    public void PerformMoveInEquippedStyle() {
        //Default key K
        if (Input.GetKeyDown(BottomAction)) {
            //perform voodoo, then jump out, you're no longer looking for input
            Debug.Log("Pressed the bottom action key");
            PlayerMoveManager.PMM.SelectedStyle[0].Attack();
            return;
        }
        //Default key J
        if(Input.GetKeyDown(LeftAction)) {

            Debug.Log("pressed the left action key");
            PlayerMoveManager.PMM.SelectedStyle[1].Attack();
            return;
        }
        //Default key L
        if (Input.GetKeyDown(RightAction)) {
            Debug.Log("Pressed the right action key");
            PlayerMoveManager.PMM.SelectedStyle[2].Attack();
            return;
        }
        //Default key I
        if (Input.GetKeyDown(TopAction)) {
            Debug.Log("Pressed the top action key");
            PlayerMoveManager.PMM.SelectedStyle[3].Attack();
            return; 
        }

    }
}
