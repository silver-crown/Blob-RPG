using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogues;
    public enum TransitioningState
    {
        WalkingState,
        BattleState,
        ShopState
    }
    /// <summary>The state which dialogue will transition into upon completion</summary>
    public TransitioningState transitioningState;
    public void TriggerDialogue() {
        Debug.Log("displaying new dialogue");
        DialogueManager.DM.StartDialogue(dialogues);
    }
}
