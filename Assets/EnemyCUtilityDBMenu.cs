using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Prototype01;

public class EnemyCUtilityDBMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI categoryName;
    [SerializeField] GameObject menu;
    //private string[] displayedUtilityCategories;

    public void DisplayUtilityDebugValues(bool display, Dictionary<string, UtilityCategory> utilityCategorydict){
        string finalUtilityString = null;
        menu.SetActive(display);


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
}
