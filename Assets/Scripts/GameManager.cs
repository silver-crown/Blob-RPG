using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : StateMachine
{
    public static GameManager GM;
    [SerializeField] GameObject Player;
    [SerializeField] MenuScript PauseMenu;
    #region Input Manager
    public KeyCode Upward { get; set;}
    public KeyCode Downward { get; set; }
    public KeyCode Left { get; set; }
    public KeyCode Right { get; set; }
    public KeyCode Pause { get; set; }
    public KeyCode Test { get; set; }

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
        Upward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ForwardKey", "W"));
        Downward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BackwardKey", "S"));
        Left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftKey", "A"));
        Right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightKey", "D"));
        Pause = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PauseKey", "Escape"));
        Test = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("TestKey", "K"));
    }
    #endregion

    #region State Machine

    private void Start() {
        SetState(new PlayerWalkingState(Player, PauseMenu));
    }
    #endregion
}
