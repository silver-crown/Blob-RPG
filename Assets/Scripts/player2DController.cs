using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class player2DController : MonoBehaviour {
        [SerializeField] public CombatPauseScript PauseMenu;
        [SerializeField] private BoxCollider2D groundDetector;
        static public GameObject Player;

        [SerializeField] private int health;

        public int lvl;
        public int exp;
        private float lastY;
        private bool attacking;
        private bool comboEnd;
        private bool chainable;
        private int currentMove = 0;
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

        [SerializeField] private partyChar myChar;

        //set as player1 keycodes if player is in control
        private KeyCode jumpKey;
        private KeyCode leftKey;
        private KeyCode rightKey;
        private KeyCode attackKey;
        private KeyCode cycleRight;


        [SerializeField] public bool playerChar;


        [SerializeField] private int experiencePoints;

         private void Start() {
            jumpCount = maxJumps;
         }


        private void Awake() {
            //If a player doesn't already exist, make this the player
            //DontDestroyOnLoad(gameObject);
            //Player = gameObject;

            //if there is a manager 
            /*else if (Player != this) {
                Destroy(gameObject);
            }*/
            PauseMenu.transform.SetParent(null);
    }

        private void Update() {
            lastY = transform.position.y;
            ChangePlayer();
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
            //CM.Jump(hero);
            if(Input.GetKeyDown(jumpKey) && jumpCount > 0){
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
            if(rb.velocity.y == 0  && !Input.GetKey(leftKey) && !Input.GetKey(rightKey) && !attacking){
                SetAnimationBools("Idle");
            }
            //Move to the left
            if(Input.GetKey(leftKey) && !attacking){
                this.transform.position -= new Vector3(moveSpeed, 0.0f, 0.0f);
                    if(rb.velocity.y == 0){
                        SetAnimationBools("Left");
                        transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.z);
                    }
            }
            //Move to the right
            if(Input.GetKey(rightKey) && !attacking){
                this.transform.position += new Vector3(moveSpeed, 0.0f, 0.0f);
                if(rb.velocity.y == 0){
                SetAnimationBools("Right");
                    transform.localScale = new Vector3(1.0f, transform.localScale.y, transform.localScale.z);
                }
            }
            if(Input.GetKey(GameManager.GM.Test)){
                //test stuff here
            }

        }


        void Attack(){
            if(Input.GetKeyDown(attackKey) && !attacking){
                currentMove = 0;
                //do the first move in combo
                SetAnimationBools(MoveList[currentMove].animationName);
                attacking = true;
                Debug.Log("executing combo move number " + (currentMove+1));
                //motion should stop, cannot resume until animation is finished or until player jumps
            }
            else if(attacking && Input.GetKeyDown(attackKey) && !chainable){
                comboEnd = true;
                Debug.Log("COMBO IS OVER, PLAYER PRESSED THE KEY TOO SOON");
                currentMove = 0;
            }
            else if(attacking && Input.GetKeyDown(attackKey) && chainable && (currentMove+1) < maxCombo){
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
    
        void ChangePlayer(){
            if(Input.GetKeyDown(cycleRight) && playerChar){
                Debug.Log("pressed the cycle key");
                Debug.Log(gameObject.name + playerChar);
                StartCoroutine(CombatManager.CM.CycleCharacters());
            }
            if(playerChar){
                jumpKey = GameManager.GM.Jump;
                leftKey = GameManager.GM.BattleLeft;
                rightKey = GameManager.GM.BattleRight;
                attackKey = GameManager.GM.Attack;
                cycleRight = GameManager.GM.CycleRight;
            } else if (!playerChar){
                jumpKey = KeyCode.None;
                leftKey = KeyCode.None;
                rightKey = KeyCode.None;
                attackKey = KeyCode.None;
                cycleRight = KeyCode.None;  
            } 
        }

        public int GetHP(){
            return health;
        }

        public void GainEXP(int exp){
            experiencePoints += exp;
        }

        public void SetUpCharStats(partyChar p){
            myChar = p;
            lvl = myChar.level;
            exp = myChar.experiencePoints;
        }

        public void IncreaseEXP(int exp){
            myChar.experiencePoints += exp;
        }
    }
