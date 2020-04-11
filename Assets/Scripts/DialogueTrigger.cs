using UnityEngine;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;

    public void TriggerDialogue() {
       if(dialogue.Length != 0) {
           foreach (Dialogue d in dialogue) {
                Debug.Log("displaying new dialogue");
                DialogueManager.DM.StartDialogue(d);
           }
       }
    }
}