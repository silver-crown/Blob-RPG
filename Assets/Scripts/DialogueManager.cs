using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager DM;
    public Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

    }

    private void Awake() {
        //If a manager doesn't already exist, make this the manager
        if (DM == null) {
            DontDestroyOnLoad(this);
            DM = this;
        }
        //if there is a manager 
        else if (DM != this) {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("Starting conversation with " + dialogue.name);
        sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        StartCoroutine(DisplayNextSentence());
    }

    public IEnumerator DisplayNextSentence() {
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        new WaitForEndOfFrame();
        ///<summary>While there's still sentences left, wait for input before displaying the next sentence</summary>
        while (true) {
            if (Input.GetKeyDown(GameManager.GM.Interact)) {
                new WaitForEndOfFrame();
                sentence = sentences.Dequeue();
                Debug.Log(sentence);
            }
                if (sentences.Count <= 0) {
                EndDialogue();
                yield break;
            }
            ///<summary>Wait for a frame</summary>
            yield return true;
        }
    }

    void EndDialogue() {
        Debug.Log("End of dialogue");
    }
}
