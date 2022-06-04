using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class player2DController : MonoBehaviour {

        [SerializeField] private BoxCollider2D groundDetector;
        private bool attacking;
        private bool comboEnd;
        private bool chainable;
        private int currentMove = 0;
        private int comboCount = 0;
        public Rigidbody2D rb;
        [SerializeField] Animator anim;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float gravity;
        [SerializeField] private float jumpForce;
        [SerializeField] public int maxJumps;
        [SerializeField] private int jumpCount;
        [SerializeField] private int maxCombo;
        [SerializeField] private Collision2D[] hitboxes;
        [SerializeField] private List<ComboAttack> MoveList;
        private float lastY;

        //list of combo moves
        List<string> comboMoveList = new List<string>
            { "atk_frontHand","atk_backHand", "atk_2h_overHand"};

         private void Start() {
            jumpCount = maxJumps;    
         }

        private void Update() {
            lastY = transform.position.y;
            Move();
            Jump();
            Fall();
            Attack();
        }
        //gravity being applied to the player character
        private void Fall(){
            if(rb.velocity.y < -0.1 && !attacking){
                SetAnimationBools("Fall");
            }
        }
        //Jump force being applied to the player character
        private void Jump(){
            if(Input.GetKeyDown(GameManager.GM.Jump) && jumpCount > 0){
                rb.velocity = new Vector2(0.0f, jumpForce);
                jumpCount --;
                SetAnimationBools("Jump");
                attacking = false;
                comboEnd = false;
                chainable = false;
            }
        }
        //movement of the player character, should only apply when knockback and such is null
        private void Move(){
            if(rb.velocity.y == 0  && !Input.GetKey(GameManager.GM.BattleLeft) && !Input.GetKey(GameManager.GM.BattleRight) && !attacking){
                SetAnimationBools("Idle");
            }
            //Move to the left
            if(Input.GetKey(GameManager.GM.BattleLeft) && !attacking){
                this.transform.position -= new Vector3(moveSpeed, 0.0f, 0.0f);
                    if(rb.velocity.y == 0){
                        SetAnimationBools("Left");
                        transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.z);
                    }
            }
            //Move to the right
            if(Input.GetKey(GameManager.GM.BattleRight) && !attacking){
                this.transform.position += new Vector3(moveSpeed, 0.0f, 0.0f);
                if(rb.velocity.y == 0){
                SetAnimationBools("Right");
                    transform.localScale = new Vector3(1.0f, transform.localScale.y, transform.localScale.z);
                }
            }

        }


        void Attack(){
            if(Input.GetKeyDown(GameManager.GM.Attack) && !attacking){
                currentMove = 0;
                //do the first move in combo
                SetAnimationBools(MoveList[currentMove].animationName);
                attacking = true;
                Debug.Log("executing combo move number " + (currentMove+1));
                //motion should stop, cannot resume until animation is finished or until player jumps
            }
            else if(attacking && Input.GetKeyDown(GameManager.GM.Attack) && !chainable){
                comboEnd = true;
                Debug.Log("COMBO IS OVER, PLAYER PRESSED THE KEY TOO SOON");
                currentMove = 0;
            }
            else if(attacking && Input.GetKeyDown(GameManager.GM.Attack) && chainable && (currentMove+1) < maxCombo){
                Debug.Log("CONTINUING COMBO");
                //else do the next move in the combo (this move +1 in an array/list of strings)
                SetAnimationBools(MoveList[++currentMove].animationName);
                Debug.Log("executing combo move number " + (currentMove+1));
            }
        }

        void SetAnimationBools(string s) {
            if(!anim.GetBool(s)) {
                ///<summary>Get all the animations in the animator</summary>
                foreach (AnimatorControllerParameter parameter in anim.parameters) {
                    ///<summary>If they're a bool, go ahead and do your magic</summary>
                    if (parameter.type == AnimatorControllerParameterType.Bool) {
                        ///<summary>If it matches the string provided, set it to true</summary>
                        if (parameter.name == s) {
                            anim.SetBool(parameter.name, true);
                        }
                        ///<summary>Else set it to false</summary>
                        else {
                            anim.SetBool(parameter.name, false);
                        }
                    }
                }
            }
        }

        public void AlertObservers(string message)
            {
                if (message.Equals("AttackAnimationEnded")){
                    attacking = false;
                    comboEnd = false;
                    chainable = false;
                    Debug.Log("attacking has stopped");
                    // Do other things based on an attack ending.
                }
                //did the player press the attack button too soon or the animation's over? end the combo
                //If the combo's not over, the player can follow up with the next attack
                if(message.Equals("Chainable") && !comboEnd){
                    chainable = true;
                    Debug.Log("move can be followed up");
                }

                if(message.Equals("hitbox")){
                    //make hitboxes,
                    Debug.Log("got hitbox message");
                    MoveList[currentMove].Attack();
                    
                }
                if(message.Equals("hitboxGone")){
                    //get rid of hitboxes
                }
            }
        private void OnCollisionEnter2D(Collision2D other) {
            if(other.transform.CompareTag("battleGround")){
                jumpCount = maxJumps;
            }
        }



    }
