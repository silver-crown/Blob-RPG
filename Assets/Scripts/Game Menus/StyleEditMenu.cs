using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyleEditMenu : GameMenu
{

    [SerializeField] GameObject MovesDropDown;
    private enum MoveToBeEdited {
        TOP, BOTTOM, LEFT, RIGHT
    }
    MoveToBeEdited mMoveToBeEdited;
    public void ReturnButtonPressed() {
        GameObject overWorldMenu = Resources.Load("OverworldPauseMenu", typeof(GameObject)) as GameObject;
        Open(overWorldMenu);
    }

    //should add a new, EMPTY style to the player's list of styles
    public void NewStyleButtonPressed() {

    }
    //Should ask for confirmation, then deletes the style currently viewed by the player, should then switch view to the next style in the list
    //should NOT work if the player has only one style in their list.
    public void DeleteStyleButtonPressed() {

    }
    //Should cycle to the left in the style list and display the style now in the center.
    //If it reaches the end then it should loop back to the other side
    public void CycleLeftButtonPressed() {

    }
    //Same as above, but to the right
    public void CycleRightButtonPressed() {

    }
    //Should handle all logic for switching the move for the selected icon.
    //should chang
    public void IconButtonPressed(string s) {
        ShowMovesDropDown();
        switch (s) {
            case "top":
                mMoveToBeEdited = MoveToBeEdited.TOP;
                Debug.Log("pressed the top button");
                break;
            case "bottom":
                mMoveToBeEdited = MoveToBeEdited.BOTTOM;
                break;
            case "left":
                mMoveToBeEdited = MoveToBeEdited.LEFT;   
                break;
            case "right":
                mMoveToBeEdited = MoveToBeEdited.RIGHT;
                break;
        }
    }

    /*Self explanatory
     The drop-down should show the UNLOCKED content of the PlayerStylesAndMoveset's Moves list */
    private void ShowMovesDropDown() {
        MovesDropDown.SetActive(true);
    }

    //Self explanatory
    private void HideMovesDropDown() {
        MovesDropDown.SetActive(false);
    }
    

    /*Method should replace the move associated with mMoveToBeEdited's current value.
     *TOP would be associated with the I key by default
     *BOTTOM would be associated with the K key by default
     *LEFT would be associated with the J key by default
     *RIGHT would be associated with the L key by default
     */
    private void ReplaceMove() {
        switch(mMoveToBeEdited) {
            case MoveToBeEdited.TOP:
                break;
            case MoveToBeEdited.BOTTOM:
                break;
            case MoveToBeEdited.LEFT:
                break;
            case MoveToBeEdited.RIGHT:
                break;
        }
    }
}
