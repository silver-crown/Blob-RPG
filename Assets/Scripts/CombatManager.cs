using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public static CombatManager CM;
    public bool Fighting;
    public bool fightIsOver;
    public Animator transition;
    [SerializeField] public GameObject blueDmgPopup;
    [SerializeField] public combatEnemySpawnPoints enemySpawnPointsScript;
    [SerializeField] public PlayerSPPositions playerSpawnPointsScript;

    [SerializeField] public TextMeshPro p1HP;

    //the overworld avatar the player interracted with before the fight
    public GameObject overworldEnemy;
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

    public enum PlayerSpawnPointPositions{
        rowOnLeftSide
    }
    //enemy spawn points
    public spawnPointPositions spPositions;

    public PlayerSpawnPointPositions pspPositions;


    //Player spawn points
    public PlayerSPPositions pPositions;

    //The enemies taken from the overworld object
    public List<GameObject> enemies = new List<GameObject>();

    //The actual enemies being instantiated
    public List<Enemy> enem = new List<Enemy>();

    //The player characters being instantiated
    public List<GameObject> playerChars = new List<GameObject>();
    List<int> usedUpSpaces = new List<int>();


    public List<GameObject> enemySpawnPoints = new List<GameObject>();

    public List<GameObject> playerSpawnPoints = new List<GameObject>();

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
            EmptyPlayerList();
            EndBattle();
            //enter victory state
            //***********************
            //show a message, exit combat and return to overworld
        }
    }

    public IEnumerator CycleCharacters(){
        //get who's next in line and set them to be the player character, set self to be AI
        for( int i = 0; i <= playerChars.Count-1; i++){
            Debug.Log(i);
            //if the character is the player character
            if(playerChars[i].GetComponent<player2DController>().playerChar == true){
                //if there's no one next in line, get the one first in line
                if(i == playerChars.Count-1){
                    playerChars[0].GetComponent<player2DController>().playerChar = true;
                    playerChars[0].GetComponent<SpriteRenderer>().color = Color.clear;
                     yield return new WaitForSeconds(0.01f);
                    playerChars[0].GetComponent<SpriteRenderer>().color = Color.white;

                //else just change the one next in line
                } else{
                    playerChars[i+1].GetComponent<player2DController>().playerChar = true;
                    playerChars[i+1].GetComponent<SpriteRenderer>().color = Color.clear;
                    yield return new WaitForSeconds(0.01f);
                    playerChars[i+1].GetComponent<SpriteRenderer>().color = Color.white;
                }
                //set self to AI
                playerChars[i].GetComponent<player2DController>().playerChar = false;
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
    //place enemies around the arena (possibly bugged)
    public void PlaceEnemies(){
        //number of spawn points in the arena
        int nmbrOfSpawnPoints = enemySpawnPoints.Count;
        //find the spawn points for enemies, and place the enemies on the points randomly
        //make list of coordinates
        List<Vector3> spCoords = new List<Vector3>();
        foreach(GameObject sp in enemySpawnPoints){
            spCoords.Add(sp.transform.position);
        }
        foreach(GameObject enemy in enemies){
            int i = 0;
            if(usedUpSpaces.Count != nmbrOfSpawnPoints){
                do{
                    i = Random.Range(0, nmbrOfSpawnPoints);
                } while(usedUpSpaces.Contains(i));
                //take a random entry from the spCoords list and instantiate an enemy there
                GameObject j = (GameObject)Instantiate(enemy, spCoords[i], Quaternion.identity);
                Enemy k = j.GetComponent<Enemy>();
                k.gameObject.SetActive(true);
                j.GetComponent<Enemy>().enabled =  !j.GetComponent<Enemy>().enabled;
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
    public void PlacePlayers(){
        //find the spawn points for players, and place them there consecutively
        List<Vector3> spCoords = new List<Vector3>();
        
        foreach(GameObject sp in playerSpawnPoints){
            spCoords.Add(sp.transform.position);
        }
        int i = 0;
        foreach(GameObject partyMember in GameManager.GM.partyChars){
            Debug.Log("party member no:" + i+1);
            GameObject k = (GameObject)Instantiate(partyMember, spCoords[i], Quaternion.identity);
            i++;

            //add to list of instantiated enemies 
            playerChars.Add(k);
        }
    }
    void EmptyEnemyList(){
        foreach(Enemy i in enem){
            Destroy(i.gameObject);
        }

        enemies.Clear();
        enemies.TrimExcess();
        enem.Clear();
        enem.TrimExcess();

    }

    void EmptyPlayerList(){
        foreach(GameObject p in playerChars){
            Destroy(p);
        }
        playerChars.Clear();
        playerChars.TrimExcess();
    }

    void SleepPlayers(){
        foreach(GameObject p in GameManager.GM.partyChars){
            p.SetActive(false);
        }
    }

    public void setUpEnemySpawnPoints(){       
        
        //search for all the objects with the correct tag
        //(((((might need some black magic for determining which spawnpoint set to use
        ///
        /// Maybe use the type and number of enemies to determine the spawn point type?))))))

        spPositions = spawnPointPositions.threeInARow;

        switch (spPositions){
            case(spawnPointPositions.threeInARow):
                //fill in list with the child object of the spawn points prefab
                int i = 0;
                foreach (Transform c in enemySpawnPointsScript.threeInARow.GetComponentsInChildren<Transform>()){
                    if(i != 0){
                        enemySpawnPoints.Add(c.gameObject);
                    }
                    i++;
                }
                break;
            default:
                break;
        }
    }

    //set up the player spawn points used for the battle
    public void setUpPlayerSpawnPoints(){
        
        pspPositions = PlayerSpawnPointPositions.rowOnLeftSide;

        switch (pspPositions){
            case(PlayerSpawnPointPositions.rowOnLeftSide):
                //fill in list with the child object of the spawn points prefab
                int i = 0;
                foreach (Transform c in playerSpawnPointsScript.rowOnLeftSide.GetComponentsInChildren<Transform>()){
                    if(i != 0){
                        playerSpawnPoints.Add(c.gameObject);
                    }
                    i++;
                }
                break;
            default:
                break;
        }
    }

    public void StartBattle(){
        Fighting = true;
        //set first in slot to be the initialplayer character
        playerChars[0].GetComponent<player2DController>().playerChar = true;
    }
    void EndBattle(){
        Debug.Log("battle ended");
        //method should exit/destroy combat scene and return to overworld
        SceneManager.UnloadScene("CombatTest");
        //enemies and player entities need to be disabled upon exit
        
        //disable the overworld guy
        overworldEnemy.SetActive(false);

        GameManager.GM.FreezeAllEntities("Player", false);
        GameManager.GM.FreezeAllEntities("Enemy", false);
        fightIsOver = true;
    }
}
