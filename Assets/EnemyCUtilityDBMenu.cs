using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyCUtilityDBMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI categoryName;
    [SerializeField] GameObject menu;
    //private string[] displayedUtilityCategories;

    public void DisplayUtilityDebugValues(bool display, Prototype01.UtilityCategory[] utilityCategories, int numOfCategories){
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

        for(int i = 0; i < numOfCategories; i++){
            Debug.Log("number of categories: " + numOfCategories);
                finalUtilityString += (utilityCategories[i].name + "   " + utilityCategories[i].weight + "\n");
                //numOfUtilities is blank, so we go only up to the penultimate number
                for(int j = 0; j <= utilityCategories[i].numOfUtilities-1; j++){
                //format the utility value
                    string s = $"{utilityCategories[i].uArray[j].weight}";
                    finalUtilityString += (utilityCategories[i].uArray[j].name + "  " + s + "\n");
                }
            
            //display each of the utility name and values for 
        }
        //combine the strings
       /* for(int i = 0; i < displayedUtilityCategories.Length; i++) {
           // finalUtilityString += displayedUtilityCategories[i];
        }*/
        categoryName.GetComponent<TextMeshProUGUI>().SetText(finalUtilityString);
    }
}
