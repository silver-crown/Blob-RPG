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
        //If a player doesn't already exist, make this the player
        if (Player == null) {
            DontDestroyOnLoad(gameObject);
            Player = gameObject;
        }
        //if there is a manager 
        else if (Player != this) {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")){

            //combat manager should take data from other
            CombatManager.CM.overworldEnemy = other.gameObject;
            CombatManager.CM.enemies.AddRange(other.gameObject.GetComponent<Entity>().enemyList);
            //transition into enemy combat
            TransitionManager.TM.transitionType = TransitionManager.TransitionType.Enemy;
            StartCoroutine(TransitionManager.TM.TransitionIntoCombat());
        }        
    }
}
