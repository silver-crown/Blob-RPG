using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Prototype01;
using UnityEngine.UI;

public class EnemyCUtilityDBMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI categoryName;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject bottomActionButton;
    [SerializeField] GameObject topActionButton;
    [SerializeField] GameObject leftActionButton;
    [SerializeField] GameObject rightActionButton;
    List<GameObject> ActionButtons = new List<GameObject>();
    //private string[] displayedUtilityCategories;
    private KeyCode BottomActionKey;
    private KeyCode TopActionKey;
    private KeyCode LeftActionKey;
    private KeyCode RightActionKey;



    private void Start() {
        BottomActionKey = GameManager.GM.BottomAttack;
        TopActionKey = GameManager.GM.TopAttack;
        LeftActionKey = GameManager.GM.LeftAttack;
        RightActionKey  = GameManager.GM.RightAttack;
        ActionButtons.Add(bottomActionButton);
        ActionButtons.Add(topActionButton);
        ActionButtons.Add(leftActionButton);
        ActionButtons.Add(rightActionButton);
    }
    public void DisplayUtilityDebugValues(bool display, Dictionary<string, UtilityCategory> utilityCategorydict){
        string finalUtilityString = null;
        menu.SetActive(display);
        foreach(GameObject b in ActionButtons) {
            b.SetActive(display);
        }
        DisplayInput();
        //Display each of the utility category names and values for the entity
        //take all the category names (with values) with their utility names+values, and make them into one string
        //with a set space between them, do this for every category
        //combine them into one string
        //display on a single textMeshProUGUI object
        /*
         * format:
         * 
         * CategoryName1: CategoryValue  |  CategoryName2: CategoryValue .......
         * UtilityName1: UtilityValue    |  UtilityName1: UtilityValue
         * UtilityName2: UtilityValue    |  UtilityName2: UtilityValue 
         * ...................................................
         * ...................................................
         * 
         */
        //do the same for the utility names and values  

        //every utility category that's going to be displayed 

        //the final string that'll be displayed on screen
        foreach(KeyValuePair<string, UtilityCategory> i in utilityCategorydict) {
            finalUtilityString += (i.Key + "  " + i.Value + "\n");
            foreach(KeyValuePair<string, Utility> j in i.Value.utilityDict) {
                finalUtilityString += j.Key + "  " + j.Value.weight + "\n";
            }
        }
        categoryName.GetComponent<TextMeshProUGUI>().SetText(finalUtilityString);
    }

    void DisplayInput() {
        //Bottom action key
        if (Input.GetKeyDown(BottomActionKey)) {
            bottomActionButton.GetComponent<Image>().color = Color.blue;
        }
        if(Input.GetKeyUp(BottomActionKey)) {
            bottomActionButton.GetComponent<Image>().color = Color.white;
        }
        //Top action key
        if (Input.GetKeyDown(TopActionKey)) {
            topActionButton.GetComponent<Image>().color = Color.blue;
        }
        if(Input.GetKeyUp(TopActionKey)) {
            topActionButton.GetComponent<Image>().color = Color.white;
        }
        //Left Action key
        if (Input.GetKeyDown(LeftActionKey)) {
            leftActionButton.GetComponent<Image>().color = Color.blue;
        }
        if(Input.GetKeyUp(LeftActionKey)) {
            leftActionButton.GetComponent<Image>().color = Color.white;
        }
        //Right Action key
        if (Input.GetKeyDown(RightActionKey)) {
            rightActionButton.GetComponent<Image>().color = Color.blue;
        }
        if(Input.GetKeyUp(RightActionKey)) {
            rightActionButton.GetComponent<Image>().color = Color.white;
        }
    }
}
