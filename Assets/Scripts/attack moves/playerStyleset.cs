using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEngine;


//This script should handle all the logic surrounding the switching of player move sets (styles) 
//A single style is a list of moves the player can perform, mapped to each of the action buttons
public class PlayerStyleset : MonoBehaviour
{
    private KeyCode action1 = GameManager.GM.Attack;
    private KeyCode action2 = GameManager.GM.SubAttack1;
    private KeyCode action3 = GameManager.GM.SubAttack2;
    private KeyCode action4 = GameManager.GM.SubAttack3;

    int selectedStyleIteration = 0;

    //A single style is a list of moves the player can perform, mapped to each of the action buttons
    private List<PlayerMove> Style = new List<PlayerMove>();
    //The selected style
    private List<PlayerMove> SelectedStyle = new List<PlayerMove>();
    //List of individual styles, customized and created by the player
    private List<List<PlayerMove>> StylesList = new List<List<PlayerMove>>();


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

    public void AddStyle(List<PlayerMove> style) {
        StylesList.Add(style);
    }
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
    public void EditStyle(int originalIndex, List<PlayerMove> edited) {
        StylesList[originalIndex] = edited;
    }

    //Method should handle logic related to pressing the different action buttons. 
    public void PerformMoveInEquippedStyle() {
        //Default key K
        if (Input.GetKey(action1)) {
            //perform voodoo, then jump out, you're no longer looking for input

            //run the action's perform move 
            SelectedStyle[0].Attack();

            return;
        }
        //Default key J
        if(Input.GetKey(action2)) {
            //run the action's perform move 
            SelectedStyle[1].Attack();
            return;
        }
        //Default key L
        if (Input.GetKey(action3)) {
            //run the action's perform move 
            SelectedStyle[2].Attack();
            return;
        }
        //Default key I
        if (Input.GetKey(action4)) {
            //run the action's perform move 
            SelectedStyle[3].Attack();
            return; 
        }

    }
}
