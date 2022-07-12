using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MenuScript : MonoBehaviour
{

    //the different menu screens
    public GameObject pauseMenu, bagMenu, equipmentMenu, statusMenu, 
                        journalMenu, saveLoadMenu, systemMenu, controlSetupMenu;
    //the different buttons for the main pause screen
    public GameObject bagButton, equipmentButton, statusButton, journalButton,
                        saveLoadButton, systemButton;     
    //The different buttons for the system menu
    public GameObject controlsButton, soundButton, quitGameButton;
    //the icon buttons for the party members
    public GameObject party1, party2, party3, party4;

    private GameObject currentMenu;

    [SerializeField] private GameObject arrow;

    private bool exitedPauseMenu = false;

    //the button the menu should return to when pressing the back button
    Dictionary<GameObject, GameObject> returnMenuButtonPair = new Dictionary<GameObject, GameObject>();
    //the button the menu should go to when entering a new menu
    Dictionary<GameObject, GameObject> menuButtonPair = new Dictionary<GameObject, GameObject>();
    //the menus that the UI should fall back to when backspace is pressed
    Dictionary<GameObject, GameObject> backToPreviousMenu = new Dictionary<GameObject, GameObject>();

    Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;
    void Awake(){
      /*  menuButtonPair.Add(bagMenu, bagButton);
        menuButtonPair.Add(equipmentMenu, equipmentButton);
        menuButtonPair.Add(statusMenu, statusButton);
        menuButtonPair.Add(journalMenu, journalButton);
        menuButtonPair.Add(saveLoadMenu, saveLoadButton);*/

        menuButtonPair.Add(systemMenu,controlsButton);
        returnMenuButtonPair.Add(systemMenu, systemButton);
        returnMenuButtonPair.Add(controlSetupMenu, controlsButton);
        backToPreviousMenu.Add(controlSetupMenu, systemMenu);
        backToPreviousMenu.Add(systemMenu, pauseMenu);

    }
    void Start()
    {
       // menuPanel = transform.Find("Panel");
        //menuPanel.gameObject.SetActive(false);
        waitingForKey = false;

        //iterate through each child, set the corresponding buttons to display the appropriate key
        //SetupMenuButtonNames();
        menuInit();
    }

    void Update(){
      /*  if (Input.GetKeyDown(GameManager.GM.Pause) && !menuPanel.gameObject.activeSelf) {
            menuPanel.gameObject.SetActive(true);
        } else if(Input.GetKeyDown(GameManager.GM.Pause)){
            menuPanel.gameObject.SetActive(false);
        }*/
        onMenuExit();
        SelectedArrow();
    }

    public void menuInit(){
        //clear selected
        EventSystem.current.SetSelectedGameObject(null);
        //select new object
        EventSystem.current.SetSelectedGameObject(bagButton);
        currentMenu = pauseMenu;
    }
    public void onMenu(GameObject mnu){
        mnu.SetActive(true);
        currentMenu = mnu;
        //clear selected
        EventSystem.current.SetSelectedGameObject(null);
        //set current menu to mnu
        EventSystem.current.SetSelectedGameObject(menuButtonPair[mnu]);
    }

    public void onMenuExit(){
        if(Input.GetKeyDown(KeyCode.Backspace)){
           
            GameObject g;
            //is there a menu underneath this one?
            if(returnMenuButtonPair.TryGetValue(currentMenu, out g)){
                currentMenu.SetActive(false);
                ///clear selected
                EventSystem.current.SetSelectedGameObject(null);
                //select new object
                EventSystem.current.SetSelectedGameObject(returnMenuButtonPair[currentMenu]);
            //set the current menu to the one being exited to, allowing for nested menus
                currentMenu = backToPreviousMenu[currentMenu];
            }
            else{
                Debug.Log("exited the main menu");
                ResetPauseMenuScreen();
                exitedPauseMenu = true;
            }
        }
    }
    public void ResetPauseMenuScreen(){
        systemMenu.SetActive(false); 
        controlSetupMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(bagButton);
    }

    public bool ExitedPauseMenu(){
        bool e = exitedPauseMenu;
        exitedPauseMenu = false;
        return e;
    }

    //put an arrow next to the object currently selected by the EventSystem
    private void SelectedArrow(){
        Debug.Log(EventSystem.current.currentSelectedGameObject);
        arrow.gameObject.transform.position = new Vector3 (EventSystem.current.currentSelectedGameObject.transform.position.x, 
                                                        EventSystem.current.currentSelectedGameObject.transform.position.y);
    }

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
                GameManager.GM.Attack = newKey;
                buttonText.text = GameManager.GM.Attack.ToString();
                PlayerPrefs.SetString("AttackKey", GameManager.GM.Attack.ToString());
                break;
            case ("Sub Attack"):
                GameManager.GM.SubAttack = newKey;
                buttonText.text = GameManager.GM.SubAttack.ToString();
                PlayerPrefs.SetString("SubAttackKey", GameManager.GM.SubAttack.ToString());
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
                    i.GetComponentInChildren<Text>().text = GameManager.GM.OverworldUpward.ToString();
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
                    i.GetComponentInChildren<Text>().text = GameManager.GM.Attack.ToString();
                    break;  
                case "SubAttackKey":
                    i.GetComponentInChildren<Text>().text = GameManager.GM.SubAttack.ToString();
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

