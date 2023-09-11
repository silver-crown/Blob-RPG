using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMoveManager : MonoBehaviour
{
    public static PlayerMoveManager PMM;
    private player2DController Player2D;

    int selectedStyleIteration = 0;


    public void SetPlayer2D(player2DController p) {
        Player2D = p;
    }
    public player2DController GetPlayer2D() {
        return Player2D;
    }
    //List of all moves available to the player, unlocked or otherwise. Should be ordered by the level they're unlocked
    public List<PlayerMove> Moves = new List<PlayerMove>();
    //A single style is a list of moves the player can perform, mapped to each of the action buttons


    //The selected style
    public PlayerMove[] SelectedStyle = new PlayerMove[4];
    //List of individual styles, customized and created by the player
    private List<PlayerMove[]> StylesList = new List<PlayerMove[]>();

    
    /*Should get the move that unlock at level 1, and insert these into the StylesList.
     SelectedStyle to be the starter style*/
    public void SetUpStarterStyle() {
        //A single style is an array of moves the player can perform, mapped to each of the action buttons
        PlayerMove[] style = new PlayerMove[4];
        foreach (PlayerMove move in Moves) {
            //get the move that unlock at level 1
            if (move.unlocksAtLevel == 1) {
                //fill all the slots in the new Style with the move
                for (int i = 0; i <= 3; i++) {
                    style[i] = move;
                }
                break;
            }
        }
        //put the style in StylesList
        StylesList.Add(style);
        //set selectedStyle to this list
        SelectedStyle = style;
    }

    /*Fills the moves list, should be ordered by the level they're unlocked
        **Should run ONCE at startup. */
    public void SetUpMovesList() {
        PlayerMove[] allAvailableMoves = GameObject.FindObjectsOfType<PlayerMove>();
        foreach (PlayerMove move in allAvailableMoves) {
            Moves.Add(move);
        }
        Moves.OrderBy(o => o.unlocksAtLevel);
    }

    void AddStyle() {

    }
    void DeleteStyle() {

    }

    private void Start() {
        SetUpMovesList();
        SetUpStarterStyle();
    }
    private void Awake() {
        //If a manager doesn't already exist, make this the manager
        if (PMM == null) {
            DontDestroyOnLoad(this);
            PMM = this;
        }
        //if there is a manager 
        else if (PMM != this) {
            Destroy(gameObject);
        }
    }
    private void Update() {
        if(Player2D != null) {
            PMM.transform.position = Player2D.transform.position;
        }
    }
}
