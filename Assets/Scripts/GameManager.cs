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

    //Player1 battle keys
    public KeyCode BattleRight { get; set; }
    public KeyCode BattleLeft { get; set; }
    public KeyCode Attack { get; set; }
    public KeyCode SubAttack1 { get; set; }
    public KeyCode SubAttack2 { get; set; }
    public KeyCode SubAttack3 { get; set; }
    public KeyCode CycleLeft { get; set; }
    public KeyCode CycleRight { get; set; }
    public KeyCode Jump { get; set; }
    public KeyCode Test { get; set; }
    
    //Player2 battle keys
    public KeyCode BattleRight2p { get; set; }
    public KeyCode BattleLeft2p { get; set; }
    public KeyCode Attack2p { get; set; }
    public KeyCode SubAttack2p { get; set; }
    public KeyCode CycleLeft2p { get; set; }
    public KeyCode CycleRight2p { get; set; }
    public KeyCode Jump2p { get; set; }

    public KeyCode Pause { get; set; }
    public KeyCode Interact { get; set; }
    public KeyCode Sprint { get; set; }
    public KeyCode Back { get; set; }

    public KeyCode DBugKey { get; set; }

    //all the available characters
    public GameObject[] characterList;
    //current party
    public List<GameObject> partyChars = new List<GameObject>();

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

        SubAttack1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SubAttackKey", "J"));
        SubAttack2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SubAttackKey", "L"));
        SubAttack3 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SubAttackKey", "I"));

        CycleLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CycleLeftKey", "Q"));
        CycleRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CycleRightKey", "E"));
        Test =  (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Test", "T"));
        //misc. controls
        Pause = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PauseKey", "Escape"));
        Interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("InteractKey", "E"));
        Sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SprintKey", "LeftShift"));
        Back  = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backKey", "Backspace"));

        //Debug Controls
        DBugKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("debugKey", "F1"));
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

    #region partymembers
    //assign character to slot in party
    void SetPartyMember(GameObject character, int slot){
        partyChars[slot] = character;
    }
    #endregion
}
