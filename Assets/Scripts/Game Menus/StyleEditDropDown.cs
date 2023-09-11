using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*This class should handle the logic for generating and displaying buttons 
 for the drop-down menu in the style editor. It should only display the UNLOCKED moves.*/
public class StyleEditDropDown : MonoBehaviour
{
    public GameObject b;
    public GameObject buttonSpawnPoint;
    public StyleEditMenu styleEditMenu;
    private void Start() {
        
    }
    /*generates all the move "buttons" available to the player. To do this it needs to have a list of
     all the moves and get the ones that are unlocked. The Moves list is taken from the PlayerStylesAndMoveset class*/
    public void GenerateMoveButtons() {
        List<GameObject> list = new List<GameObject>();
        for( int i = 0; i < PlayerMoveManager.PMM.Moves.Count; i++) {
            GameObject k = Instantiate(b);
            k.transform.position = new Vector3(buttonSpawnPoint.transform.position.x, buttonSpawnPoint.transform.position.y);
            if(i > 0) {
                k.transform.position = new Vector3(buttonSpawnPoint.transform.position.x, list[i-1].transform.position.y - 40);
            }
            k.transform.SetParent(transform, true);
            k.transform.localScale = Vector3.one;
            k.GetComponentInChildren<Text>().text = PlayerMoveManager.PMM.Moves[i].animationName;
            string s = "hellow";
            k.GetComponent<Button>().onClick.AddListener(delegate { OnClick(k.GetComponentInChildren<Text>().text);});
            list.Add(k);

        }
    }
    void OnClick(string s) {
        Debug.Log("Entered onclick for " + s + " using delegate");
        //replaceMove from the style edit menu should run now
        styleEditMenu.ReplaceMove(s);
    }

}
