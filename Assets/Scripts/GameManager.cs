using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public KeyCode Forward { get; set;}
    public KeyCode Backward { get; set; }
    public KeyCode Left { get; set; }
    public KeyCode Right { get; set; }

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
        Forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ForwardKey", "W"));
        Backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BackwardKey", "S"));
        Left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftKey", "A"));
        Right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightKey", "D"));
    }
}
