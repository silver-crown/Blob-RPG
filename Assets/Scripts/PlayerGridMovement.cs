using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class for player movement across a grid-by-grid system.
/// </summary>
public class PlayerGridMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintModifier;
    [SerializeField] public GameObject movePoint;
    [SerializeField] public GameObject frontPoint;
    [SerializeField] Animator anim;
    [SerializeField] float gridLength;
    /// <summary>The direction the player is currently looking</summary>
    string direction;
    /// <summary> How long a button han been pressed for</summary>
    float pressTimer;
    /// <summary>The threshold for the press timer </summary>
    [SerializeField] float pressTimerThreshold;
    //Dictates whether or not the sprint key is a toggle
    bool usingToggleSprint;
    bool sprinting;
    // Start is called before the first frame update
    void Start()
    {
        if(direction == null) {
            direction = "Down";
        }
        if(pressTimerThreshold == 0) {
            pressTimerThreshold = 0.5f;
        }
        if(gridLength == 0) {
            gridLength = 1.0f;
        }
        if(moveSpeed == 0) {
            moveSpeed = 2.0f;
        }
        if(sprintModifier == 0){
            sprintModifier = 1;
        }
        movePoint.transform.SetParent(null);
        frontPoint.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ///<summary>Which direction the the player looking?</summary>
        LookingInDirection(direction);
    }


    void playerInput() {


    }

    /// <summary>
    /// Function for moving across the grid in a linear fashion, no diagonal movements allowed
    /// </summary>
    void Move() {
        if(movePoint.GetComponent<PlayerFrontColliderCheck>().WalkingIntoWall) {
            Debug.Log("Player walking into a wall!");
            movePoint.transform.position = transform.position;
            ///<summary>Add a check for if you're walking into a scene loader here, if so, abort everything</summary>
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, moveSpeed * sprintModifier * Time.deltaTime);
        }


        //Moving around in the game, done by moving the movepoints around.

        ///<summary>The player can only move to the next point on the map if they've already reached their current point</summary>
        if (Vector3.Distance(transform.position, movePoint.transform.position) <= 0.0f) {
            ///<summary>Set the animator to start animating</summary>
            anim.speed = 1;
            ///<summary> Moving Up on the map</summary>
            if (Input.GetKey(GameManager.GM.OverworldUpward)) {
                pressTimer += Time.deltaTime;
                if(pressTimer >= pressTimerThreshold) {
                    movePoint.transform.position += new Vector3(0.0f, gridLength, 0.0f);
                    frontPoint.transform.position = movePoint.transform.position;
                }
                SetAnimationBools("Walking Up");
                direction = "Up";
            } 
            ///<summary> Moving Down on the map</summary>
            else if (Input.GetKey(GameManager.GM.OverworldDownward)) {
                pressTimer += Time.deltaTime;
                if (pressTimer >= pressTimerThreshold) {
                    movePoint.transform.position += new Vector3(0.0f, -gridLength, 0.0f);
                    frontPoint.transform.position = movePoint.transform.position;
                }
                SetAnimationBools("Walking Down");
                direction = "Down";
            }
            ///<summary> Moving Right on the map</summary>
            else if (Input.GetKey(GameManager.GM.OverworldLeft)) {
                pressTimer += Time.deltaTime;
                if (pressTimer >= pressTimerThreshold) {
                    movePoint.transform.position += new Vector3(-gridLength, 0.0f, 0.0f);
                    frontPoint.transform.position = movePoint.transform.position;
                }
                SetAnimationBools("Walking Left");
                direction = "Left";
            }
            ///<summary> Moving Right on the map</summary>
            else if (Input.GetKey(GameManager.GM.OverworldRight)) {
                pressTimer += Time.deltaTime;
                if (pressTimer >= pressTimerThreshold) {
                    movePoint.transform.position += new Vector3(gridLength, 0.0f, 0.0f);
                }
                SetAnimationBools("Walking Right");
                direction = "Right";
            }
            else {
                SetAnimationBools("Idle");
                pressTimer = 0;
            }
        }
        Sprint();
    }
    ///<summary>
    ///Method controlling the sprint key's functionality
    ///<summary>
    void Sprint() {
        //if the sprint key is a hold, not a toggle
        if(!usingToggleSprint){
            if (Input.GetKeyDown(GameManager.GM.Sprint)) {
                sprintModifier = 2;
                anim.speed = 2;
            }
            if (Input.GetKeyUp(GameManager.GM.Sprint)) {
                sprintModifier = 1;
                anim.speed = 1;
            }
        }
        //else if it's a toggle
        else{
            if (Input.GetKeyDown(GameManager.GM.Sprint)) {
                //if the player is sprinting
                if(!sprinting){
                    sprintModifier = 2;
                    anim.speed = 2;
                    sprinting = true;
                }
                else{
                    sprintModifier = 1;
                    anim.speed = 1;  
                    sprinting = false;
                }
            }
        }
    }
    /// <summary>
    /// Helper method for toggling sprint functionality
    /// </summary>
    public void ToggleSprint(){
        if(usingToggleSprint){
            usingToggleSprint = false;
        }
        else{
            usingToggleSprint = true;
        }
    }
    /// <summary>
    /// A function for setting the animation bools for the animator, because I'm a lazy bum.
    /// It makes the player animate properly when walking around
    /// </summary>
    /// <param name="s"></param>
    /*void SetAnimationBools(string s) {
        switch (s) {
            ///<summary>If the player is walking up</summary>
            case ("Walking Up"):
                if(!anim.GetBool("Walking Up")) {
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
            ///<summary>If the player is walking down</summary>
            case ("Walking Down"):
                if(!anim.GetBool("Walking Down")) {
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
            ///<summary>If the player is walking left</summary>
            case ("Walking Left"):
                if(!anim.GetBool("Walking Left")) {
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
            case ("Walking Right"):
                if (!anim.GetBool("Walking Right")) {
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

        }
    }
    */
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
    
    /// <summary>
    /// For use whenever the animation should stop for something important like leaving the state
    /// </summary>
    public void PauseAnimation() {
        SetAnimationBools("Idle");
    }

    /// <summary>
    /// For setting the frontPoint in the direction the player is currently looking, this is needed primarily for interaction 
    /// </summary>
    /// <param name="direction"></param>
    void LookingInDirection(string direction) {
        switch (direction) {
            case ("Up"):
                frontPoint.transform.position = new Vector3(transform.position.x, transform.position.y + gridLength, 0.0f);
                break;
            case ("Down"):
                frontPoint.transform.position = new Vector3(transform.position.x, transform.position.y - gridLength, 0.0f);
                break;
            case ("Left"):
                frontPoint.transform.position = new Vector3(transform.position.x - gridLength, transform.position.y, 0.0f);
                break;
            case ("Right"):
                frontPoint.transform.position = new Vector3(transform.position.x + gridLength, transform.position.y, 0.0f);
                break;
        }
    }
    /// <summary>
    /// Tries to return the gameobject the frontpoint is collding with, if one exists.
    /// </summary>
    /// <returns></returns>
    public GameObject CanIInteractWithThis() {
        return frontPoint.GetComponent<InteractionFrontCollisionCheck>().Interactable;
    }
}