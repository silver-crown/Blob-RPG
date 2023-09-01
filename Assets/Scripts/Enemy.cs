using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int mana;
    [SerializeField] private int defense;
    [SerializeField] private int poise;
    [SerializeField] private bool hurt;
    [SerializeField] private int EXPValue;
    [SerializeField] private bool dead;
    float blinkingTimer = 0.05f;
        
    private void Update() {
        //check if the entity has more HP left
        if(getHP() <= 0){
            dead = true;
        }

        if (hurt){
            blinkingTimer += Time.deltaTime;
            sprite.color =  Color.clear;
            if(blinkingTimer >= 0.05f){
                blinkingTimer = 0.0f;
                hurt = false;
                sprite.color = Color.white;
            }
        }
        //if it's dead do dead stuff
        if (dead) {
            CombatManager.CM.DistributeEXP(getEXP());
            ImDead();
        }
    }

    //
    public int Hurt(int damage){
        hurt = true;
        int damageDealt = damage - defense;
        Debug.Log("ENEMY WAS HURT");
        health -= (damageDealt);
        return damageDealt;
    }



    public int getHP(){
        return health;
    }
    public int getMaxHP()
    {
        return maxHealth;
    }

    public int getEXP(){
        return EXPValue;
    }
    #region AI actions

    public virtual void MeleeAttack() { }
    public virtual void MoveToClosestEnemy() { }
     
    public void ImDead() { this.gameObject.SetActive(false); }


    #endregion
}
