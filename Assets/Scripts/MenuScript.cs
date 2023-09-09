using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*This script should handle all logic related to traversal of menus, pause screen or otherwise.
 * The menus themselves (settings menu, style setup, main pause screen, opening menu) should all be preloaded
 * prefabs that can be called whenever needed with a Show(myMenu) method or something similar. A setActive(myChild) approach carries
 * unnecessary overhead and is a generally inefficent and sloppy way of doing things, this script's purpose is to solve that problem.
 */
public class MenuScript : MonoBehaviour
{
   /* private GameMenu currentMenu;

    //the different menu screens
    public GameObject pauseMenu, bagMenu, equipmentMenu, statusMenu, 
                        journalMenu, saveLoadMenu, systemMenu, controlSetupMenu;
    //the different buttons for the main pause screen
    public GameObject bagButton, equipmentButton, statusButton, journalButton,
                        saveLoadButton, systemButton;     
    //The different buttons for the system menu
    public GameObject controlsButton, soundButton, quitGameButton;

    //the different tab buttons for the control setup menu
    public GameObject tabTestButton, tabTestButton2, tabTestButton3;
    //the icon buttons for the party members
    public GameObject party1, party2, party3, party4;


    private GameObject currentlySelectedItem;

    private GameObject lastSelectedNavButton;

    [SerializeField] private GameObject arrow;

    private bool exitedPauseMenu = false;

    //the button the menu should return to when pressing the back button
    Dictionary<GameObject, GameObject> returnMenuButtonPair = new Dictionary<GameObject, GameObject>();
    //the button the menu should go to when entering a new menu
    Dictionary<GameObject, GameObject> menuButtonPair = new Dictionary<GameObject, GameObject>();
    //the menus that the UI should fall back to when backspace is pressed
    Dictionary<GameObject, GameObject> backToPreviousMenu = new Dictionary<GameObject, GameObject>();

    List<GameObject> navigationButtons = new List<GameObject>();

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
   /*
        menuButtonPair.Add(systemMenu,controlsButton);
        menuButtonPair.Add(controlSetupMenu,tabTestButton);
        returnMenuButtonPair.Add(systemMenu, systemButton);
        returnMenuButtonPair.Add(controlSetupMenu, controlsButton);
        backToPreviousMenu.Add(controlSetupMenu, systemMenu);
        backToPreviousMenu.Add(systemMenu, pauseMenu);

        navigationButtons.AddRange(new List<GameObject>{bagButton, equipmentButton, statusButton, 
        journalButton, saveLoadButton, systemButton,controlsButton, soundButton, quitGameButton,tabTestButton});
    }
    void Start()
    {
        waitingForKey = false;

        //iterate through each child, set the corresponding buttons to display the appropriate key
        menuInit();
    }

    void Update(){
        onMenuExit();
        SelectedArrow();
        GameObject e;
        if(returnMenuButtonPair.TryGetValue(currentlySelectedItem, out e)){
            Debug.Log("return menu button pair contains" + EventSystem.current.currentSelectedGameObject);
        }
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
        //set current menu to mnu
        currentMenu = mnu;
        //clear selected
        EventSystem.current.SetSelectedGameObject(null);
        //set selected menu to mnu
        EventSystem.current.SetSelectedGameObject(menuButtonPair[mnu]);
        lastSelectedNavButton = menuButtonPair[mnu];
    }

    public void onMenuExit(){
        if(Input.GetKeyDown(KeyCode.Backspace)){
            GameObject g;
            //is there a menu underneath this one?
            if(returnMenuButtonPair.TryGetValue(currentMenu, out g)){
                //if the currently selected object isn't a navigation button, return to the previously selected navigation button  
                if (!navigationButtons.Contains(EventSystem.current.currentSelectedGameObject)){
                    Debug.Log("foobar!");
                    EventSystem.current.SetSelectedGameObject(null);
                    //select new object
                    EventSystem.current.SetSelectedGameObject(lastSelectedNavButton); 
                }else {
                    currentMenu.SetActive(false);
                    ///clear selected
                    EventSystem.current.SetSelectedGameObject(null);
                    //select new object
                    EventSystem.current.SetSelectedGameObject(returnMenuButtonPair[currentMenu]);
                    //set the current menu to the one being exited to, allowing for nested menus
                    currentMenu = backToPreviousMenu[currentMenu];
                }
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
        //Debug.Log(EventSystem.current.currentSelectedGameObject);
        currentlySelectedItem = EventSystem.current.currentSelectedGameObject;
        Transform var = currentlySelectedItem.transform;
        arrow.gameObject.transform.position = new Vector3 (var.position.x + var.GetComponent<RectTransform>().sizeDelta.x /2, 
                                                        var.position.y);
    }

    /*Handles "opening new menu" logic
    *Should run every time a new menu is opened
    */
   /* private void OpenMenu(GameMenu m) {
        //unload current menu
        CloseMenu(currentMenu);
        //open/load menu m
    }
    private void CloseMenu(GameMenu m) {
        //hide/unload/remove menu m
    }
    */
}

