using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager DM;
    public Queue<string> sentences;
    /// <summary>Index of current dialogue being displayed</summary>
    int currentDialogue;
    /// <summary>Simple variable to check that it's done displaying dialogues</summary>
    public bool done;
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
    /// <summary>
    /// Start the chain of dialogues.
    /// </summary>
    /// <param name="dialogues"></param>
    public void StartDialogue(Dialogue[] dialogues) {
        Debug.Log("Starting conversation with " + dialogues[currentDialogue].name);
        sentences.Clear();
        ///Set the current dialogue's sentences to be the sentences ready to be displayed
        foreach (string sentence in dialogues[currentDialogue].sentences) {
            sentences.Enqueue(sentence);
        }
        StartCoroutine(DisplayNextSentence(dialogues));
    }
    /// <summary>
    /// Display the next sentence in the dialogue array.
    /// </summary>
    /// <param name="dialogues"></param>
    /// <returns></returns>
    public IEnumerator DisplayNextSentence(Dialogue[] dialogues) {
        string sentence;
        if (currentDialogue == 0) {
            Debug.Log(currentDialogue);
            sentence = sentences.Dequeue();
            Debug.Log(sentence);
            new WaitForEndOfFrame();
        }
        ///<summary>While there's still sentences left, wait for input before displaying the next sentence</summary>
        while (true) {
            ///<summary>if the interact button is pressed, there's no sentences left, but there's still dialogues</summary>
            if (Input.GetKeyDown(GameManager.GM.Interact)) {
                yield return 0;
                if (sentences.Count <= 0) {
                    ///<summary>If there's no sentences left, but there's still dialogues</summary>
                    if (currentDialogue != dialogues.Length - 1) {
                        currentDialogue++;
                        Debug.Log("No more sentences, but there's still dialogues left.");
                        StartDialogue(dialogues);
                        yield break;
                    }
                    ///<summary>If there's no sentences or dialogues left</summary>
                    else {
                        Debug.Log("No more sentences or dialogues, ending chain.");
                        EndDialogue();
                        yield break;
                    }
                }
                ///<summary>If there were no special conditions</summary>
                else {
                    Debug.Log("Key was pressed");
                    sentence = sentences.Dequeue();
                    Debug.Log(sentence);
                }
            }
            ///<summary>Wait for a frame</summary>
            yield return true;
        }
    }

    void EndDialogue() {
        Debug.Log("End of dialogue chain");
        currentDialogue = 0;
        done = true;
    }
}
