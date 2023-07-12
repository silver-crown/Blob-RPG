using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using System.Linq;


//This script should be attached to a enemy entity with a separate controller script that dictates it's actions
//it's primary function is to calculate the utility value of each action, evaluate these, and finally tell the entity what action to perform 

//********************************************************
//This script utilizes arrays instead of linked lists for Utility management, this is due to lists having faster insertion/deletion, times but arrays -
//- having faster access times. The idea is that because there will be no actual insertions during runtime, but a large amount accessing and -
//- calculations on utility factors, an array solution should promote a faster decision making process for the entities during runtime.
//********************************************************
public class Prototype01 : MonoBehaviour
{
    [SerializeField] private EnemyCUtilityDBMenu debugMenu;
    bool displayingDBMenu;
    private KeyCode debugKey;
    int maxValue= 100;
    int k = 3;
    
    public struct Utility{
        public string name;
        public float weight;
        //if a utility is in the pool of best possible utilities, best should be true
        public bool isBest;
        public Utility(string n) : this(){
            name = n;
        }
        public void SetWeight(float i) {
            weight = i; 
        }
    }

    public struct UtilityCategory{
        public Utility[] uArray;
        //the utilities belonging to the category
        public Dictionary<string, Utility> utilityDict;
        public string name;
        public float weight;
        public bool isBest;
        public int numOfUtilities;
        public UtilityCategory(string n) : this(){
            name = n;
            utilityDict = new Dictionary<string, Utility>();
        }
        public void SetWeight(float i) {
            weight = i;
        }
    }
    //Utility utility = new Utility();
    //UtilityCategory utilityCategory = new UtilityCategory();

    //Array of the AI's utilities and their categories
    public int categoryArraySize;
    int maxSize = 100;
    private int numOfCategories = 0;
    public UtilityCategory[] categoriesArray { get; set;}

    //All the utility categories
    public Dictionary<string, UtilityCategory> utilityCategoriesDict;

    void Awake(){
        debugKey = GameManager.GM.DBugKey;
        categoriesArray = new UtilityCategory[maxSize];
        for(int i = 0; i < categoriesArray.Length; i++){
            categoriesArray[i].uArray = new Utility[maxSize];
        }
        utilityCategoriesDict = new Dictionary<string, UtilityCategory>();

    }

    void Update()
    {
        if (Input.GetKeyDown(debugKey)){
            Debug.Log("debug key was pressed");
            displayingDBMenu = !displayingDBMenu;
        }
        DisplayUtilities(displayingDBMenu);
        PerformAction();
    }

    //perform utility action (will be used during runtime) using a weight-based random on the best -
    // - remaining utilities after proper elimination of useless utilities has been performed
    void PerformAction(){


        GetComponent<TestPersonality>().GetCloserToPlayer();
    }
    #region Math
    //calculate the utility of the action or action category (will be used during runtime) uses a quadratic curve
    float CalculateWeight(float input, float max, float k){
        //a normalization equation, large value of k will have very little impact for low values of x
        return (float)Math.Pow(input / max, k);
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
    /*
    ***********************
    The idea here is that you don't want to perform the best possible utilities every time, as it will come
    across as robotic, instead roll a dice on the top utilities 
    ***********************
    */
    List<Utility> BestRemainingUtilities(){
        //eliminate the options with a weight of 0


        //DO NOT makes a new list for this, that's a costly operation to perform
        //tagging the "best" utilities seems to be a good option for this
        return null;
    }

    //create a utility action and put it into the category's list of utilities, also creates a category if one does not exist (won't be used during runtime)
    //bit of a messy logic, can probably clean this using delegates or lambdas 

    void CreateUtility(string n, string c){
        Utility u = new Utility(n);
        //check if a category with the name c exists in the categoriesArray array
        //if it does exist, add the newly created utility to the category's uAray array 
        //if it doesn't, create the category with the name c, then put the newly created utility to the category's uAray array 

        //There is a category with this name
        if (utilityCategoriesDict.ContainsKey(c)){
            //The category does not contain this utility
            if (!utilityCategoriesDict[c].utilityDict.ContainsKey(n)){
                //Add it to the category's dictionary
                utilityCategoriesDict[c].utilityDict.Add(n, u);
            }
        }
        //else there's no category, make one and insert the newly made utility
        else {
            utilityCategoriesDict.Add(c, new UtilityCategory(c));
            utilityCategoriesDict[c].utilityDict.Add(n, u);
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
    public void AssignUtilityValue(string n, string c, float x){
        if (utilityCategoriesDict[c].utilityDict.ContainsKey(n)) {
            var i = utilityCategoriesDict[c].utilityDict[n];
            i.weight = CalculateWeight(x, maxValue, k);
            utilityCategoriesDict[c].utilityDict[n] = i;
        }
        else {
            Debug.Log(utilityCategoriesDict[c].name + " does not contain utility " + n);
        }

    }
    //Give utility category value (will be used during runtime)
    public void AssignCategoryValue(string n, int x){
        if (utilityCategoriesDict.ContainsKey(n)) {
            var i = utilityCategoriesDict[n];
            i.weight = CalculateWeight(x, maxValue, k);
            utilityCategoriesDict[n] = i;
        }
        else {
            Debug.Log("category " + n + " does not exist");
        }
    }
    public void MakeTestUtility(string n, string c){
        CreateUtility(n, c);
    }

    //Debug function, should not be utilized during real play 
    //displays a window with all the entity's utilities  divided into categories, displaying their names and weights when a button is pressed
    void DisplayUtilities(bool display){
      /*  Debug.Log("displaying entity utility values");
        Debug.Log("second category name: " + categoriesArray[1].name);
        Debug.Log("second category number of utilities: " + categoriesArray[1].numOfUtilities);*/
        debugMenu.DisplayUtilityDebugValues(display, utilityCategoriesDict);
    }
}
