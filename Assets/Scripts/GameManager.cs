using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : StateMachine
{
    public static GameManager GM;
    [SerializeField] GameObject Player;
    #region Input Manager
    public KeyCode OverworldUpward { get; set;}
    public KeyCode OverworldDownward { get; set; }
    public KeyCode OverworldLeft { get; set; }
    public KeyCode OverworldRight { get; set; }

    public KeyCode BattleRight { get; set; }
    public KeyCode BattleLeft { get; set; }
    public KeyCode Attack { get; set; }
    public KeyCode SubAttack { get; set; }
    public KeyCode CycleLeft { get; set; }
    public KeyCode CycleRight { get; set; }
    public KeyCode Jump { get; set; }
    public KeyCode Test { get; set; }
    
    public KeyCode Pause { get; set; }
    public KeyCode Interact { get; set; }
    public KeyCode Sprint { get; set; }

    private void Awake() {
       //If a manager doesn't already exist, make this the manager
        if (GM == null) {
            DontDestroyOnLoad(this);
            GM = this;
        }
        //if there is a manager 
        else if (GM != this) {
            Destroy(gameObject);
        }
        //assign the keys based on player preferences, defaults to value given otherwise.

        //Overworld controls
        OverworldUpward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("OverworldUpwardKey", "W"));
        OverworldDownward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("OverworldDownwardKey", "S"));
        OverworldLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("OverworldLeftKey", "A"));
        OverworldRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("OverworldRightKey", "D"));
        //Combat controls
        BattleLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CombatLeftKey", "A"));
        BattleRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CombatRightKey", "D"));
        Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CombatJumpKey", "Space"));
        Attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AttackKey", "K"));
        SubAttack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SubAttackKey", "O"));
        CycleLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CycleLeftKey", "Q"));
        CycleRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CycleRightKey", "E"));
        Test =  (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Test", "T"));
        //misc. controls
        Pause = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PauseKey", "Escape"));
        Interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("InteractKey", "E"));
        Sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SprintKey", "LeftShift"));
    }
    #endregion

    #region State Machine

    private void Start() {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        SetState(new OverworldState());
    }
    /// <summary>
    /// Freeze all entities with tag s
    /// </summary>
    public void FreezeAllEntities(string s, bool freeze){
        if(freeze){
            Debug.Log("freezing all entities with tag " + s);
        }
        else{
             Debug.Log("unfreezing all entities with tag " + s);
        }
        GameObject[] tagArray = GameObject.FindGameObjectsWithTag(s);  
        foreach (var obj in tagArray) {
            Behaviour[] behaviour = obj.GetComponents<Behaviour>();
                for(int i = 0; i < behaviour.Length; i++) {
                    behaviour[i].enabled = !freeze;
                    if((s == "2DPlayer" || s == "2DEnemy") && freeze){
                       behaviour[i].GetComponent<Animator>().enabled = true;
                    }
                }
            if(!freeze){
                if (obj.GetComponent<Rigidbody2D>()){
                    obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                    if(s == "Player" || s == "Enemy"){
                        obj.GetComponent<Rigidbody2D>().useFullKinematicContacts = !freeze;
                    }
                }
            }
        }
    
    }
    #endregion
}
