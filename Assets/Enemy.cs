﻿using System.Collections;
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
    float blinkingTimer = 0f;
    private void Update() {

        if (hurt){
            blinkingTimer += Time.deltaTime;
            sprite.color =  Color.clear;
            if(blinkingTimer >= 0.05f){
                blinkingTimer = 0.0f;
                hurt = false;
                sprite.color = Color.white;
            }
        }
}

    public int Hurt(int damage){ 
        hurt = true;
        int damageDealt = damage - defense;
        Debug.Log("ENEMY WAS HURT");
        health -= (damageDealt);
        return damageDealt;
    }
}
