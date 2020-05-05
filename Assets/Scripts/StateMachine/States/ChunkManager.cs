using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The script that handles dynamic loading of game objects near and far from the player
/// </summary>
public class ChunkManager : MonoBehaviour
{
    Vector2 chunkPosition = new Vector2(0,0);
    GameObject[] gameObjects;
    List <GameObject> activeGameObjects = new List<GameObject>();
    List <GameObject> shouldBeActiveGameObjects = new List<GameObject>();

    public int chunkSize = 2;
    List <Collider2D> collisions  = new List<Collider2D>();
    ContactFilter2D cf;
    private void Awake() {
        //Get all the objects in the scene
         gameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
    }
    private void Update() {
        ///<summary>If the player has moved a chunk, start loading</summary>
        if (Vector2.Distance(gameObject.transform.position, chunkPosition) >= chunkSize) {
            List<Collider2D> oldCollisions = new List<Collider2D>();
            Physics2D.OverlapCircle(transform.position, chunkSize, cf, collisions);
            LoadObjects(collisions);
          //  UnloadObjects(oldCollisions);
            chunkPosition = gameObject.transform.position;
        }
        ///<summary>Else tell it to fuck off</summary>
    }

    /// <summary>
    /// Start loading objects in the chunk
    /// </summary>
    void LoadObjects(List <Collider2D> results) {

        ///Check if the result isn't active, it should be active
        foreach (Collider2D r in results) {

            //add gameobjects to list of objects that should be on.
            shouldBeActiveGameObjects.Add(r.gameObject);
            //Compare them to the list of gameobjects that are on
            //turn off the ones that are only in the second list, remove them from that list
            if(activeGameObjects.Contains(r.gameObject) && !shouldBeActiveGameObjects.Contains(r.gameObject)) {
                r.gameObject.SetActive(false);
                activeGameObjects.Remove(r.gameObject);
            }
            
            //turn on the ones in the first list
            //Add to the second list, empty the first list to finish things off.
           
        }

        //Ha liste over game objekter som er på
        //en ny liste over objekter som skal være på
        //hvis noen er i den øverste og ikke i den nederste så skal de skrus av og dyttes ut av listen

        foreach (GameObject g in shouldBeActiveGameObjects) {
            g.SetActive(true);
        }

        List <Behaviour[]> behaviour = new List<Behaviour[]>();
        foreach (Collider2D r in results) {
            behaviour.Add(r.GetComponents<Behaviour>());
            if(r.gameObject.GetComponent<SpriteRenderer>() != null) {
                r.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        foreach (Behaviour[] b in behaviour) {
            foreach (Behaviour b2 in b) {
                b2.enabled = true;
            }
        }
    }
    /// <summary>
    /// Unload objects from the chunk
    /// </summary>
    /// <param name="results"></param>
    void UnloadObjects(List <Collider2D> results) {
        foreach (Collider2D r in results) {
            r.gameObject.SetActive(false);
        }
    }
}

