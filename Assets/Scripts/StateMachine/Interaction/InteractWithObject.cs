using UnityEngine;

/// <summary>
/// Monobehaviour class for interacting with the object in front of the player, if one is present.
/// </summary>
public class InteractWithObject : MonoBehaviour
{
    GameObject FrontPoint;
    [System.NonSerialized] public GameObject Interactable;
    private void Start() {
        ///<summary>Get the frontPoint from the player</summary>
        if(GetComponent<PlayerGridMovement>() != null) {
            FrontPoint = GetComponent<PlayerGridMovement>().frontPoint;
        }
    }

    ///<summary>If the object frontpoint is colliding with is a valid interactable, send a cool debug message!</summary>
    public void Interact() {
        if (FrontPoint.GetComponent<InteractionFrontCollisionCheck>().Interactable != null) {
            Debug.Log("It's an interactable object!");
        }
        else {
            Debug.Log("This is not an interactable object...");
        }
    }
}
