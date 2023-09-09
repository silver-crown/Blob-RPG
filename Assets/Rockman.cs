using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockman : Enemy
{
    Rigidbody2D rb;
    Animator m_Animator;
    [SerializeField] private float jumpForce = 0f;
    bool cannotAttack;


    private void Awake() {
        m_Animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public override void MeleeAttack() {

        Debug.Log("Rock is attacking");
        m_Animator.ResetTrigger("walking");

        //stand still, you're no longer able to move

        //do a silly idle animation
        m_Animator.SetTrigger("Idle");
        //jump
            rb.velocity = new Vector2(0.0f, jumpForce);
            if(rb.velocity.y < 0.0f) {
                m_Animator.ResetTrigger("Jumping");
                m_Animator.SetTrigger("Falling");
            }
        //land and cause destruction
        
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
