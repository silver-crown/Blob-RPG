using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script taking care of the very generalized specifications of the player
/// The pause menu, enemy, dialogue box, etc.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] public MenuScript PauseMenu;
    [SerializeField] public GameObject MovePoint;
    static public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.transform.SetParent(null);
    }
    private void Awake() {
        //If a manager doesn't already exist, make this the manager
        if (Player == null) {
            DontDestroyOnLoad(this);
            Player = gameObject;
        }
        //if there is a manager 
        else if (Player != this) {
            Destroy(gameObject);
        }
    }
}
