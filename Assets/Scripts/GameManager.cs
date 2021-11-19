using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //misc. controls
        Pause = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PauseKey", "Escape"));
        Interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("InteractKey", "E"));
        Sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SprintKey", "LeftShift"));
    }
    #endregion

    #region State Machine

    private void Start() {
        SetState(new OverworldState());
    }
    #endregion
}
