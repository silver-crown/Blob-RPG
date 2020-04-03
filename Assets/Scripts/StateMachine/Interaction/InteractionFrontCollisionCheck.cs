using UnityEngine;

public class InteractionFrontCollisionCheck : MonoBehaviour
{
    public GameObject Interactable;
    /// <summary>
    /// if there's something interactable there, set the interactable 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Interactable>() != null) {
            Interactable = collision.gameObject;
        }
    }
    /// <summary>
    ///Set the interactable back to null if there's no longer anything of interest there
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Interactable>() != null) {
            Interactable = null;
        }
    }
}