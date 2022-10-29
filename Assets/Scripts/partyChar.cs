using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partyChar : MonoBehaviour
{
    public GameObject character;
    public string pName;
    public int experiencePoints;
    public int expToBeGained;
    public int level;
    public enum statusEffects{
        poison,
        dizzy
    };

    public void IncreaseTempEXP(int exp){
        expToBeGained += exp;
    }
    public void IncreaseEXP(int exp){
        experiencePoints += exp;
    }
    
}
