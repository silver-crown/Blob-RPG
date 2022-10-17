using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Entity : MonoBehaviour    
{
    public string entityName;

    //List of enemies to be spread around the battle arena
    public List<GameObject> enemyList = new List<GameObject>();

    //this class needs to give data to the combat scene
    //this is done by having the combat manager take the enemyList as input

}