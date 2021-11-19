using UnityEngine;

public class player2DMovement : MonoBehaviour {

        [SerializeField] private float moveSpeed;
        [SerializeField] private float gravity;
        [SerializeField] private float jumpForce;
        [SerializeField] public int maxJumps;
        private int jumpCount;

        //gravity being applied to the player character
        private void Gravity(){
            transform.position -= new Vector3(0.0f, gravity, 0.0f);    
        }
        //Jump force being applied to the player character
        private void Jump(){
            if(Input.GetKeyDown(GameManager.GM.Jump)){
                transform.position += new Vector3(0.0f, jumpForce, 0.0f);
            }
        }
        //movement of the player character, should only apply when knockback and such is null
        private void Move(){
            //Move to the left
            if(Input.GetKey(GameManager.GM.BattleLeft)){
                transform.position -= new Vector3(moveSpeed, 0.0f, 0.0f);
            }
            //Move to the right
            if(Input.GetKey(GameManager.GM.BattleRight)){
                transform.position += new Vector3(moveSpeed, 0.0f, 0.0f);
            }

        }
        //this one is gonna take some time
        //private void Attack(){}
    }