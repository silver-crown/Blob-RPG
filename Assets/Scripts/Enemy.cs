using System.Collections;
using System.Collections.Generic;
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
    private bool dead;
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
}

    public int Hurt(int damage, GameObject attacker){ 
        hurt = true;
        int damageDealt = damage - defense;
        Debug.Log("ENEMY WAS HURT");
        health -= (damageDealt);
        if(dead){
            attacker.GetComponent<player2DController>().IncreaseTempEXP(getEXP());
            ImDead();
        }
        return damageDealt;
    }

    private void ImDead(){
        //give player the exp and disable enemy ****add death animation later****

        this.gameObject.SetActive(false);
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

    public bool amIDead(){
        return dead;
    }
}
