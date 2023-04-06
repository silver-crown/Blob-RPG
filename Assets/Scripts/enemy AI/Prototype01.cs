using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//This script should be attached to a enemy entity with a separate controller script that dictates it's actions
//it's primary function is to calculate the utility value of each action, evaluate these, and finally tell the entity what action to perform 
public class Prototype01 : MonoBehaviour
{
    int maxValue= 100;
    int k = 3;
    
    public struct Utility{
        public string name;
        public double weight;
        //if a utility is in the pool of best possible utilities, best should be true
        public bool isBest;
        public Utility(string n) : this(){
            name = n;
        }
    }

    public struct UtilityCategory{
        public Utility[] uArray;
        public string name;
        public double weight;
        public bool isBest;
        public int numOfUtilities;
        public UtilityCategory(string n) : this(){
            name = n;
        }
    }
    //Utility utility = new Utility();
    //UtilityCategory utilityCategory = new UtilityCategory();

    //Array of the AI's utilities and their categories
    public int categoryArraySize;
    int maxSize = 100;
    public UtilityCategory[] categoriesArray { get; set;}
   
    void Awake(){
        categoriesArray = new UtilityCategory[maxSize];
        for(int i = 0; i < categoriesArray.Length; i++){
            categoriesArray[i].uArray = new Utility[maxSize];
        }
    }

    //perform utility action (will be used during runtime) using a weight-based random on the best remaining utilities after proper elimination has been performed
    void PerformAction(){

   }
    #region Math Bullshit
    //calculate the utility of the action or action category (will be used during runtime) uses a quadratic curve
    double CalculateWeight(double input, double max, float k){
        //a normalization equation, large value of k will have very little impact for low values of x
        return Math.Pow(((double)input/(double)max),k);
    }
    #endregion

    //find the category with the highest weight, tags it as the best one and returns it (linear search)
    UtilityCategory CategoryWithHighestWeight(){
        //simply tags the best category
        if(categoriesArray.Length > 1){
            for (int i = 1; i < categoriesArray.Length; i++){
                if(categoriesArray[i].weight > categoriesArray[i-1].weight){
                    categoriesArray[i].isBest = true; 
                    categoriesArray[i-1].isBest = false;
                }
            }
            foreach (UtilityCategory category in categoriesArray){
                if(category.isBest == true){
                    return category;
                }
            }
        }
        return categoriesArray[0];
    }
    
    //find the best utility, eliminate the ones that are much much worse, and return the ones that are left
    List<Utility> BestRemainingUtilities(){
        //eliminate the options with a weight of 0
        //find the best utility, eliminate the ones that are much much worse, and return the ones that are left

        //DO NOT makes a new list for this, that's a costly operation to perform
        //tagging the "best" utilities seems to be the best option
        return null;
    }

    //create a utility action and put it into the category's list of utilities, also creates a category if one does not exist (won't be used during runtime)
    void CreateUtility(string n, string c){

        Utility u = new Utility(n);
        
        //check if there's a category with the name c, if there isn't, create one
        
        for(int i = 0; i < categoryArraySize; i++){
            if(categoriesArray[i].name == c){

                //put the newly created utility into the uArray of c
                int j = categoriesArray[i].numOfUtilities++;
                categoriesArray[i].uArray[j] = u;
                break;
            }
            else{
                //if there's no category with the name, create it
                if(i == categoryArraySize){
                    categoriesArray[++categoryArraySize] = new UtilityCategory(c);
                    //place the new utility in the array
                    categoriesArray[categoryArraySize].uArray[categoriesArray[categoryArraySize].numOfUtilities++] = u;
                }
            }
        }
        //if there's no categories at all yet, make one
        if(categoryArraySize == 0){
            categoriesArray[categoryArraySize] = new UtilityCategory(c);
            //initialize the category's utility array
            categoriesArray[categoryArraySize].uArray = new Utility[maxSize];
            //place the new utility in the array
            categoriesArray[categoryArraySize].uArray[categoriesArray[categoryArraySize++].numOfUtilities++] = u;
        }
    }

    Utility GetUtility(string n){
        for(int i = 0; i < categoryArraySize; i++){
            for(int j = 0; j < categoriesArray[i].numOfUtilities; i++){
                    if (categoriesArray[i].uArray[j].name == n){
                        return categoriesArray[i].uArray[j];
                }
            }
        }
        return categoriesArray[0].uArray[0];
    }
    UtilityCategory GetCategory(string n){
        for(int i = 0; i < categoryArraySize; i++){
            if(categoriesArray[i].name == n){
                return categoriesArray[i];
            }
        }
        return categoriesArray[0];
    }

    //Give utility value to action or category (will be used during runtime)
    public void AssignUtilityValue(string n, int x){
        for(int i = 0; i < categoryArraySize; i++){
            for(int j = 0; j < categoriesArray[i].uArray.Length; j++){
                if (categoriesArray[i].uArray[j].name == n){
                    categoriesArray[i].uArray[j].weight = CalculateWeight(x, maxValue, k);
                    Debug.Log("utility weight:" + categoriesArray[i].uArray[j].weight);
                }
            }
        }
    }
    //Give utility category value (will be used during runtime)
    public void AssignCategoryValue(string n, int x){
        for(int i = 0; i < categoryArraySize; i++){
            if(categoriesArray[i].name == n){
                categoriesArray[i].weight = CalculateWeight(x, maxValue, k);
                Debug.Log("category name: " + categoriesArray[i].name);
                Debug.Log("category weight: " + (categoriesArray[i].weight));
                Debug.Log(categoriesArray[i].uArray[1].name);
            break;
            }
        }
    }
    public void MakeTestUtility(string n, string c){
        CreateUtility(n, c);
    }

    //Debug function, should not be utilized during real play 
    void DisplayUtilities(){
        //display a window with all the entity's utilities  divided into categories, displaying their names and weights
       /* int i = 0;
        foreach(GameObject partyMem in CombatManager.CM.playerChars){
        playerHealths[i].GetComponent<TextMeshProUGUI>().SetText(partyMem.GetComponent<player2DController>().GetHP().ToString());
        i++;
        }*/
    }
}
