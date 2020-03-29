using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class for player movement across a grid-by-grid system.
/// </summary>
public class PlayerGridMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject movePoint;
    [SerializeField] Animator anim;
    [SerializeField] float gridLength;
    /// <summary> How long a button han been pressed for</summary>
    float pressTimer;
    /// <summary>The threshold for the press timer </summary>
    [SerializeField] float pressTimerThreshold;
    // Start is called before the first frame update
    void Start()
    {
        if(pressTimerThreshold == 0) {
            pressTimerThreshold = 0.5f;
        }
        if(gridLength == 0) {
            gridLength = 1.0f;
        }
        if(moveSpeed == 0) {
            moveSpeed = 2.0f;
        }
        movePoint.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// Function for moving across the grid in a linear fashion, no diagonal movements allowed
    /// </summary>
    void Move() {
        if(movePoint.GetComponent<PlayerFrontColliderCheck>().WalkingIntoWall) {
            Debug.Log("Player walking into a wall!");
            movePoint.transform.position = transform.position;
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, moveSpeed * Time.deltaTime);
        }

        ///<summary> Moving Up on the map</summary>
        if (Vector3.Distance(transform.position, movePoint.transform.position) <= 0.0f) {
            ///<summary>Set the animator to start animating</summary>
            anim.speed = 1;
            if (Input.GetKey(GameManager.GM.Upward)) {
                pressTimer += Time.deltaTime;
                if(pressTimer >= pressTimerThreshold) {
                    movePoint.transform.position += new Vector3(0.0f, gridLength, 0.0f);
                    
                }
                SetAnimationBools("Walking Up");
            } 
            ///<summary> Moving Down on the map</summary>
            else if (Input.GetKey(GameManager.GM.Downward)) {
                pressTimer += Time.deltaTime;
                if (pressTimer >= pressTimerThreshold) {
                    movePoint.transform.position += new Vector3(0.0f, -gridLength, 0.0f);
                }
                SetAnimationBools("Walking Down");
            }
            ///<summary> Moving Right on the map</summary>
            else if (Input.GetKey(GameManager.GM.Left)) {
                pressTimer += Time.deltaTime;
                if (pressTimer >= pressTimerThreshold) {
                    movePoint.transform.position += new Vector3(-gridLength, 0.0f, 0.0f);
                }
                SetAnimationBools("Walking Left");
            }
            ///<summary> Moving Right on the map</summary>
            else if (Input.GetKey(GameManager.GM.Right)) {
                pressTimer += Time.deltaTime;
                if (pressTimer >= pressTimerThreshold) {
                    movePoint.transform.position += new Vector3(gridLength, 0.0f, 0.0f);
                }
                SetAnimationBools("Walking Right");
            }
            else {
                SetAnimationBools("Idle");
                pressTimer = 0;
            }
        }
    }
    /// <summary>
    /// A function for setting the animation bools for the animator, because I'm a lazy bum.
    /// It makes the player animate properly when walking around
    /// </summary>
    /// <param name="s"></param>
    void SetAnimationBools(string s) {
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
}