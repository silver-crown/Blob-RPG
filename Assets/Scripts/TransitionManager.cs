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

    public Animator whiteFadeTransition;
    public float transitionTime = 1.0f;
    private GameObject[] doors;
    public bool Transitioning;
    public enum TransitionType{
        Normal,
        Enemy,
        Boss,
        WhiteFade
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
        GameManager.GM.FreezeAllEntities("Player", true);
        GameManager.GM.FreezeAllEntities("Enemy", true);
        transition.gameObject.SetActive(true);
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
        transition.gameObject.SetActive(false);
        GameManager.GM.FreezeAllEntities("Player", false);
        GameManager.GM.FreezeAllEntities("Enemy", false);
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
    }

    //transition into battle and enter combat state
    public IEnumerator TransitionIntoCombat(){
        //fade into combat state
        Debug.Log("transitioning into combat");
        GameManager.GM.FreezeAllEntities("Player", true);
        GameManager.GM.FreezeAllEntities("Enemy", true);
        combatTransition.gameObject.SetActive(true);
        combatTransition.ResetTrigger("End");
        combatTransition.SetTrigger("Start");
        Transitioning = true;
        yield return new WaitForSeconds(transitionTime);
        //combat manager should here set everything from background to enemy list and player list before anything else happens
        //load combat scene
        SceneManager.LoadScene("CombatTest", LoadSceneMode.Additive);
        CombatManager.CM.setUpEnemySpawnPoints();
        CombatManager.CM.PlaceEnemies();
        CombatManager.CM.setUpPlayerSpawnPoints();
        CombatManager.CM.PlacePlayers();
        
        //needs to wait a small amount, because otherwise the freeze happens before the scene is properly loaded
         yield return new WaitForSeconds(0.001f);
        GameManager.GM.FreezeAllEntities("2DPlayer", true);
        GameManager.GM.FreezeAllEntities("2DEnemy", true);
        combatTransition.ResetTrigger("Start");
        combatTransition.SetTrigger("End");
        yield return new WaitForSeconds(transitionTime);
        combatTransition.gameObject.SetActive(false);
        Transitioning = false;
        //transition is done, players and enemies can move now
        GameManager.GM.FreezeAllEntities("2DPlayer", false);
        GameManager.GM.FreezeAllEntities("2DEnemy", false);
        CombatManager.CM.StartBattle();
        //?????????????
    }

   public IEnumerator TransitionIntoResultScreen(){
        Debug.Log("TRANSITIONING INTO RESULT SCREEN");
        whiteFadeTransition.gameObject.SetActive(true);
        whiteFadeTransition.ResetTrigger("End");
        whiteFadeTransition.SetTrigger("Start");
        Transitioning = true;
        yield return new WaitForSeconds(transitionTime);
        PartyManager.PM.addingEXPToChars = true;
        SceneManager.UnloadScene("CombatTest");
        SceneManager.LoadScene("ResultScreen", LoadSceneMode.Additive);
        CombatManager.CM.EmptyPlayerList();
        yield return 0;
    }
    public IEnumerator TransitionIntoOverworldFromBattle(){
        SceneManager.UnloadScene("ResultScreen");
        whiteFadeTransition.gameObject.SetActive(true);
        whiteFadeTransition.ResetTrigger("Start");
        whiteFadeTransition.SetTrigger("End");
        Transitioning = true;
        yield return new WaitForSeconds(transitionTime);
        GameManager.GM.FreezeAllEntities("Player", false);
        GameManager.GM.FreezeAllEntities("Enemy", false);
        CombatManager.CM.fightIsOver = true;
        whiteFadeTransition.gameObject.SetActive(false);
        yield return 0;
    }
}

