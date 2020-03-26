using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public KeyCode Upward { get; set;}
    public KeyCode Downward { get; set; }
    public KeyCode Left { get; set; }
    public KeyCode Right { get; set; }
    public KeyCode Pause { get; set; }

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
    }

    /// <summary>
    /// returns an integer based on the input given to from the player
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int GetGameManagerAxis(string s) {
        switch (s) {
            case ("Horizontal"):
                if (Input.GetKey(GM.Left)) {
                    return -1;
                }
                if (Input.GetKey(GM.Right)) {
                    return 1;
                }
                break;
            case ("Vertical"):
                if (Input.GetKey(GM.Upward)) {
                    return 1;
                }
                if (Input.GetKey(GM.Downward)) {
                    return -1;
                }
                break;
            default:
                Debug.Log("Not a game manager axis");
                break;
        }
        return 0;
    }
}
