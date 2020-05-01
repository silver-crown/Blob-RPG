using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    /// <summary>
    /// The various types of interactable objects
    /// </summary>
    public enum InteractableType
    {
        Item,
        NPC,
        Shop,
        Sign
    }
    public InteractableType interactableType;
    /// <summary>
    /// Interact with whatever it is you're interacting with
    /// </summary>
    public void InteractWith() {
        ///<summary>Trigger the asociated dialogue</summary>
        //dialogueTrigger.TriggerDialogue();

        if (interactableType == InteractableType.Item) {
            GetComponent<InteractableItem>().Interact();
        }
    }
}
