using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockman : Enemy
{
    Animator m_Animator;
    private void Awake() {
        m_Animator = gameObject.GetComponent<Animator>();
    }
    public override void MeleeAttack() {
        Debug.Log("Rock is attacking");
        m_Animator.ResetTrigger("walking");
    }

    public override void MoveToClosestEnemy() {
        //just toggle the tag
        m_Animator.SetTrigger("walking");

    }

    /*public override void ImDead() {
        //give player the exp and disable enemy ****add death animation later****

        this.gameObject.SetActive(false);
    }*/
}
