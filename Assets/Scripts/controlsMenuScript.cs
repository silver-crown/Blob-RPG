﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class controlsMenuScript : MonoBehaviour
{

    Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;
    bool waitingForKey;
     private void OnGUI() {
        keyEvent = Event.current;

        if(keyEvent.isKey && waitingForKey) {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName) {
        if (!waitingForKey) {
            StartCoroutine(AssignKey(keyName));
        }
    }
    public void SendText(Text text) {
        buttonText = text;
    }

    IEnumerator WaitForKey() {
        while (!keyEvent.isKey) {
            yield return null;
        }
    }
    public IEnumerator AssignKey(string keyName) {
        waitingForKey = true;

        yield return WaitForKey();

        switch (keyName) {
            case ("Up"):
                GameManager.GM.OverworldUpward = newKey;
                buttonText.text = GameManager.GM.OverworldUpward.ToString();
                PlayerPrefs.SetString("OverworldUpwardKey", GameManager.GM.OverworldUpward.ToString());
                break;
            case ("Down"):
                GameManager.GM.OverworldDownward = newKey;
                buttonText.text = GameManager.GM.OverworldDownward.ToString();
                PlayerPrefs.SetString("OverworldBackwardKey", GameManager.GM.OverworldDownward.ToString());
                break;
            case ("Left"):
                GameManager.GM.OverworldLeft = newKey;
                buttonText.text = GameManager.GM.OverworldLeft.ToString();
                PlayerPrefs.SetString("OverworldLeftKey", GameManager.GM.OverworldLeft.ToString());
                break;
            case ("Right"):
                GameManager.GM.OverworldRight = newKey;
                buttonText.text = GameManager.GM.OverworldRight.ToString();
                PlayerPrefs.SetString("OverworldRightKey", GameManager.GM.OverworldRight.ToString());
                break;
            case ("Pause"):
                GameManager.GM.Pause = newKey;
                buttonText.text = GameManager.GM.Pause.ToString();
                PlayerPrefs.SetString("PauseKey", GameManager.GM.Pause.ToString());
                break;
            case ("Sprint"):
                GameManager.GM.Sprint = newKey;
                buttonText.text = GameManager.GM.Sprint.ToString();
                PlayerPrefs.SetString("SprintKey", GameManager.GM.Sprint.ToString());
                break;
            case ("Combat Left"):
                GameManager.GM.BattleLeft = newKey;
                buttonText.text = GameManager.GM.BattleLeft.ToString();
                PlayerPrefs.SetString("CombatLeftKey", GameManager.GM.BattleLeft.ToString());
                break;
            case ("Combat Right"):
                GameManager.GM.BattleRight = newKey;
                buttonText.text = GameManager.GM.BattleRight.ToString();
                PlayerPrefs.SetString("CombatRightKey", GameManager.GM.BattleRight.ToString());
                break;
            case ("Jump"):
                GameManager.GM.Jump = newKey;
                buttonText.text = GameManager.GM.Jump.ToString();
                PlayerPrefs.SetString("CombatJumpKey", GameManager.GM.Jump.ToString());
                break;
            case ("Attack"):
                GameManager.GM.BottomAttack = newKey;
                buttonText.text = GameManager.GM.BottomAttack.ToString();
                PlayerPrefs.SetString("AttackKey", GameManager.GM.BottomAttack.ToString());
                break;
            case ("Sub Attack1"):
                GameManager.GM.TopAttack = newKey;
                buttonText.text = GameManager.GM.TopAttack.ToString();
                PlayerPrefs.SetString("SubAttackKey1", GameManager.GM.TopAttack.ToString());
                break;
            case ("Sub Attack2"):
                GameManager.GM.LeftAttack = newKey;
                buttonText.text = GameManager.GM.LeftAttack.ToString();
                PlayerPrefs.SetString("SubAttackKey2", GameManager.GM.LeftAttack.ToString());
                break;
            case ("Sub Attack3"):
                GameManager.GM.RightAttack = newKey;
                buttonText.text = GameManager.GM.RightAttack.ToString();
                PlayerPrefs.SetString("SubAttackKey3", GameManager.GM.RightAttack.ToString());
                break;
            case ("Cycle Left"):
                GameManager.GM.CycleLeft = newKey;
                buttonText.text = GameManager.GM.CycleLeft.ToString();
                PlayerPrefs.SetString("CycleLeftKey", GameManager.GM.CycleLeft.ToString());
                break;
            case ("Cycle Right"):
                GameManager.GM.CycleRight = newKey;
                buttonText.text = GameManager.GM.CycleRight.ToString();
                PlayerPrefs.SetString("CycleRightKey", GameManager.GM.CycleRight.ToString());
                break;
            
        }
        yield return null;
    }

    /// <summary>
    /// Iterate through the list of names, if it matches, set the button name.
    /// </summary>
    /// <returns></returns>
    void SetupMenuButtonNames() {
        Transform[] allPanelChildren = menuPanel.GetComponentsInChildren<Transform>();
        foreach(Transform i in allPanelChildren){
            switch(i.name){
                case "OverworldUpwardKey":
                    i.GetComponentInChildren<TextMeshPro>().text = GameManager.GM.OverworldUpward.ToString();
                    break;
                case "OverworldDownwardKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.OverworldDownward.ToString();
                    break;
                case "OverworldLeftKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.OverworldLeft.ToString();
                    break;
                case "OverworldRightKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.OverworldRight.ToString();
                    break;
                case "PauseKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.Pause.ToString();
                    break;  
                case "SprintKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.Sprint.ToString();
                    break;  

                //combat controls
                case "CombatLeftKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.BattleLeft.ToString();
                    break;  
                case "CombatRightKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.BattleRight.ToString();
                    break;  
                case "CombatJumpKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.Jump.ToString();
                    break;  
                case "AttackKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.BottomAttack.ToString();
                    break;  
                case "SubAttackKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.TopAttack.ToString();
                    break;  
                case "CycleLeftKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.CycleLeft.ToString();
                    break;  
                case "CycleRightKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.CycleRight.ToString();
                    break;  
            }
        }
    }


    public void ToggleButtons(string keyName){
        switch(keyName){
            case "ToggleSprint":
            Debug.Log("heil sprintler");
            break;
        }
    }
    //iterate through the list of names, it it matches. do the thing
    List<string> ButtonNames() {
        List<string> buttonNames = new List<string>();
        buttonNames.Add("ForwardKey");
        buttonNames.Add("BackwardKey");
        buttonNames.Add("LeftKey");
        buttonNames.Add("RightKey");
        buttonNames.Add("PauseKey");
        return buttonNames;
    }
    //^^^^ Idk what this does, it's never used for anything ever.
}
