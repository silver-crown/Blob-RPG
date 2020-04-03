using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InteractWith() {
        if(interactableType == InteractableType.Item) {
            GetComponent<InteractableItem>().Interact();
        }
    }
}
