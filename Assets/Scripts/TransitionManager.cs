using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager TM;
    public string transitionSpot;
    public Animator transition;
    public float transitionTime = 1.0f;
    private GameObject[] doors;
    public bool Transitioning;
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
        SceneManager.LoadScene(scene);
        SceneManager.sceneLoaded += OnSceneLoaded;
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
            if (d.GetComponent<Transitioner>().transitionSpot == transitionSpot) {
                PlayerController.Player.transform.position = new Vector3(d.transform.GetChild(0).position.x, d.transform.GetChild(0).position.y, d.transform.GetChild(0).position.z);
                PlayerController.Player.GetComponent<PlayerGridMovement>().movePoint.transform.position = PlayerController.Player.transform.position;
            }
        }
        transition.ResetTrigger("Start");
        transition.SetTrigger("End");
        Transitioning = false;
    }
}

