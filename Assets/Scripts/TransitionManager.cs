using PixelCrushers.SceneStreamer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager TM;
    public string transitionSpot;
    //transition animation for doors, cave entrances etc.
    public Animator transition;
    //transition animation for battles
    public Animator combatTransition;
    //transition animator for boss fights
    public Animator bossTransition;
    public float transitionTime = 1.0f;
    private GameObject[] doors;
    public bool Transitioning;
    public enum TransitionType{
        Normal,
        Enemy,
        Boss
    }
    public TransitionType transitionType;
    private void Awake() {
        //If a manager doesn't already exist, make this the manager
        if (TM == null) {
            DontDestroyOnLoad(this);
            TM = this;
        }
        //if there is a manager 
        else if (TM != this) {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Load the level/scene the player is moving into
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    public  IEnumerator LoadLevel(string scene) {
        transition.ResetTrigger("End");
        transition.SetTrigger("Start");
        Transitioning = true;
        yield return new WaitForSeconds(transitionTime);
        SceneStreamer.SetCurrentScene(scene);
        ///<summary>find the object that contains the transition spot, and send the player there</summary>
        doors = GameObject.FindGameObjectsWithTag("Transitioner");
        foreach (GameObject d in doors) {
            Debug.Log(d.name);
            if (d.GetComponent<Transitioner>().AreaName == transitionSpot) {
                PlayerController.Player.transform.position = new Vector3(d.transform.GetChild(0).position.x, d.transform.GetChild(0).position.y, d.transform.GetChild(0).position.z);
                PlayerController.Player.GetComponent<PlayerGridMovement>().movePoint.transform.position = PlayerController.Player.transform.position;
            }
        }
        transition.ResetTrigger("Start");
        transition.SetTrigger("End");
        yield return new WaitForSeconds(transitionTime);
        Transitioning = false;
        // SceneManager.sceneLoaded += OnSceneLoaded;
    }
    /// <summary>
    /// Once the scene has been loaded find the spot the player is supposed to go to and send him there.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        ///<summary>find the object that contains the transition spot, and send the player there</summary>
        doors = GameObject.FindGameObjectsWithTag("Transitioner");
        foreach (GameObject d in doors) {
            Debug.Log(d.name);
            if (d.GetComponent<Transitioner>().AreaName == transitionSpot) {
                PlayerController.Player.transform.position = new Vector3(d.transform.GetChild(0).position.x, d.transform.GetChild(0).position.y, d.transform.GetChild(0).position.z);
                PlayerController.Player.GetComponent<PlayerGridMovement>().movePoint.transform.position = PlayerController.Player.transform.position;
            }
        }
        transition.ResetTrigger("Start");
        transition.SetTrigger("End");
        Transitioning = false;
    }

    //transition into battle and enter combat state
    public IEnumerator TransitionIntoCombat(){
        //fade into combat state
        combatTransition.ResetTrigger("End");
        combatTransition.SetTrigger("Start");
        Transitioning = true;
        yield return new WaitForSeconds(transitionTime);
        //load combat scene
        SceneManager.LoadScene("CombatTest", LoadSceneMode.Additive);
        combatTransition.ResetTrigger("Start");
        combatTransition.SetTrigger("End");
        yield return new WaitForSeconds(transitionTime);
        Transitioning = false;
    }
}

