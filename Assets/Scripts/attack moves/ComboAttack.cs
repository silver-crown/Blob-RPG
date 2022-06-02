using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    [SerializeField] private string moveName;
    [SerializeField] private int experience; 
    [SerializeField] private int level;
    [SerializeField] private int expToNextLevel;
    [SerializeField] private int damage;
    public string animationName;
    [SerializeField] private bool isUnlocked;
    [SerializeField] protected bool attacking;
    [SerializeField] public CircleCollider2D[] hitboxes; 

    public virtual void Attack(){
        //activate hitboxes and do funky stuff
    }

    void LevelUp(){
    }
    
}
