using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public static CombatManager CM;
    public bool Fighting;
    public Animator transition;
    [SerializeField] public GameObject blueDmgPopup;
    [SerializeField] public combatEnemySpawnPoints enemySpawnPointsScript;

    [SerializeField] public TextMeshPro p1HP;
    public enum Backdrops{
        Forest,
        Field,
        Water,
        Beach,
        Desert,
        Volcano,
        MountainTop,
        House,
        Castle,
        Town
    }
    public Backdrops backdrop;

    public enum spawnPointPositions{
        threeInARow,
        twoOnTheGroundOneInTheAir,
        Boss
    }
    public spawnPointPositions spPositions;

    //The enemies taken from the overworld object
    public List<Enemy> enemies = new List<Enemy>();

    //The actual enemies being instantiated
    public List<Enemy> enem = new List<Enemy>();
    List<int> usedUpSpaces = new List<int>();


    public List<GameObject> spawnPoints = new List<GameObject>();

    //the party
    static public GameObject Player;
    
    private void Awake() {
        //If a manager doesn't already exist, make this the manager
        if (CM == null) {
            DontDestroyOnLoad(this);
            CM = this;
        }
        //if there is a manager 
        else if (CM != this) {
            Destroy(gameObject);
        }
    }
    //

    private void Update(){

        if(Input.GetKeyDown(KeyCode.H)){
            PlaceEnemies();
        }
        if(Fighting && AllEnemiesAreDead()){
            Fighting = false;
            EmptyEnemyList();
            EndBattle();
            //enter victory state
            //***********************
            //show a message, exit combat and return to overworld
        }
    }

    public IEnumerator CycleCharacters(){
        //get who's next in line and set them to be the player character, set self to be AI
        for( int i = 0; i <= GameManager.GM.partyChars.Length-1; i++){
            Debug.Log(i);
            //if the character is the player character
            if(GameManager.GM.partyChars[i].GetComponent<player2DController>().playerChar == true){
                //if there's no one next in line, get the one first in line
                if(i == GameManager.GM.partyChars.Length-1){
                    GameManager.GM.partyChars[0].GetComponent<player2DController>().playerChar = true;
                    GameManager.GM.partyChars[0].GetComponent<SpriteRenderer>().color = Color.clear;
                     yield return new WaitForSeconds(0.01f);
                    GameManager.GM.partyChars[0].GetComponent<SpriteRenderer>().color = Color.white;

                //else just change the one next in line
                } else{
                    GameManager.GM.partyChars[i+1].GetComponent<player2DController>().playerChar = true;
                    GameManager.GM.partyChars[i+1].GetComponent<SpriteRenderer>().color = Color.clear;
                    yield return new WaitForSeconds(0.01f);
                    GameManager.GM.partyChars[i+1].GetComponent<SpriteRenderer>().color = Color.white;
                }
                //set self to AI
                GameManager.GM.partyChars[i].GetComponent<player2DController>().playerChar = false;
                yield break;
            } 
        }
    }

    //Returns true if all the enemies have no health
    bool AllEnemiesAreDead(){
        foreach (Enemy enemy in enem){
            if (enemy.getHP() > 0){
                return false;
            }
        }
        Debug.Log("all enemies are dead");
        return true;
    }
    //set the background for the battle
    void SetBackground(Backdrops bd){
        backdrop = bd;
    }
    //add enemies to the enemy list (for example by coming into contact with an enemy)
    public void AddToEnemyList(Enemy e){
        enemies.Add(e);
    }
    //place enemies around the arena (possibly bugged)
    public void PlaceEnemies(){
        //number of spawn points in the arena
        int nmbrOfSpawnPoints = spawnPoints.Count;
        //find the spawn points for enemies, and place the enemies on the points randomly
        //make list of coordinates
        List<Vector3> spCoords = new List<Vector3>();
        foreach(GameObject sp in spawnPoints){
            spCoords.Add(sp.transform.position);
        }
        foreach(Enemy enemy in enemies){
            int i = 0;
            if(usedUpSpaces.Count != nmbrOfSpawnPoints){
                do{
                    i = Random.Range(0, nmbrOfSpawnPoints);
                } while(usedUpSpaces.Contains(i));
                //take a random entry from the spCoords list and instantiate an enemy there
                Enemy k = (Enemy)Instantiate(enemy, spCoords[i], Quaternion.identity);
                //add to list of instantiated enemies 
                enem.Add(k);
                //spawn point is used up
                usedUpSpaces.Add(i);
            }
            
        }
        
    }
    //add player characters to the list of players (possibly not needed, can just grab them from the current party)
    void AddToPlayerList(){

    }
    //place players around the arena
    void PlacePlayers(){

    }
    void EmptyEnemyList(){
        enemies.Clear();
        enemies.TrimExcess();
    }

    public void setUpSpawnPoints(){       
        
        //search for all the objects with the correct tag
        //(((((might need some black magic for determining which spawnpoint set to use
        ///
        /// Maybe use the type and number of enemies to determine the spawn point type?))))))

        spPositions = spawnPointPositions.threeInARow;

        switch (spPositions){
            case(spawnPointPositions.threeInARow):
                //fill in list with the child object of the spawn points prefab
                foreach (Transform c in enemySpawnPointsScript.threeInARow.GetComponentsInChildren<Transform>()){
                    spawnPoints.Add(c.gameObject);
                }
                break;
            default:
                break;
        }
    }

    public void StartBattle(){
        Fighting = true;
    }
    void EndBattle(){
        Debug.Log("battle ended");
        //method should exit/destroy combat scene and return to overworld
        SceneManager.UnloadScene("CombatTest");
    }
}
