using UnityEngine;

public class player2DMovement : MonoBehaviour {

        private bool attacking;
        public Rigidbody2D rb;
        [SerializeField] Animator anim;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float gravity;
        [SerializeField] private float jumpForce;
        [SerializeField] public int maxJumps;
        [SerializeField] private int jumpCount;
         private float lastY;

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
               // jumpCount --;
                SetAnimationBools("Jump");
                attacking = false;
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
            if(Input.GetKeyDown(GameManager.GM.Attack)){
                Debug.Log("pressed the attack button");
                SetAnimationBools("atk_frontHand");
                attacking = true;
                //motion should stop, cannot resume until animation or finished or until player jumps
            }
        }
        void SetAnimationBools(string s) {
            switch (s) {
                ///<summary>If the player is walking left</summary>
                case ("Left"):
                    if(!anim.GetBool("Left")) {
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
                    break;
                ///<summary>If the player is walking right</summary>
                case ("Right"):
                    if (!anim.GetBool("Right")) {
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
                    break;
                ///<summary>If the player is jumping</summary>
                case ("Jump"):
                    if (!anim.GetBool("Jump")) {
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
                    break;
                    ///<summary>If the player is falling</summary>
                case ("Fall"):
                    if (!anim.GetBool("Fall")) {
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
                    break;
                ///<summary>If the player is idle</summary>
                case ("Idle"):
                    if (!anim.GetBool("Idle")) {
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
                    break;

                ///<summary>If the player is doing with a front hand attack</summary>
                case ("atk_frontHand"):
                    if (!anim.GetBool("atk_frontHand")) {
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
                    break;

            }

        }

        public void AlertObservers(string message)
            {
                if (message.Equals("AttackAnimationEnded"))
                {
                    attacking = false;
                    // Do other things based on an attack ending.
                }
            }
    }