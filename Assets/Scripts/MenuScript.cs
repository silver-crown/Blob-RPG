using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;

    void Start()
    {
        menuPanel = transform.FindChild("Panel");
        menuPanel.gameObject.SetActive(false);
        waitingForKey = false;
        //iterate through each child, set the corresponding buttons to display the appropriate key
        for(int i = 0; i < 4; i++) {
            if(menuPanel.GetChild(i).name == "ForwardKey") {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Forward.ToString();
            }
            else if(menuPanel.GetChild(i).name == "BackwardKey") {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Backward.ToString();
            }
            else if (menuPanel.GetChild(i).name == "LeftKey") {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Left.ToString();
            }
            else if (menuPanel.GetChild(i).name == "RightKey") {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Right.ToString();
            }
        }
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf) {
            menuPanel.gameObject.SetActive(true);
        } else if(Input.GetKeyDown(KeyCode.Escape)){
            menuPanel.gameObject.SetActive(false);
        }
    }

    private void OnGUI() {
        keyEvent = Event.current;

        if(keyEvent.isKey && waitingForKey) {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName) {
        if (!waitingForKey) {
            StartCoroutine(AssignKey(keyName));
        }
    }
    public void SendText(Text text) {
        buttonText = text;
    }

    IEnumerator WaitForKey() {
        while (!keyEvent.isKey) {
            yield return null;
        }
    }
    public IEnumerator AssignKey(string keyName) {
        waitingForKey = true;

        yield return WaitForKey();

        switch (keyName) {
            case ("Forward"):
                GameManager.GM.Forward = newKey;
                buttonText.text = GameManager.GM.Forward.ToString();
                PlayerPrefs.SetString("ForwardKey", GameManager.GM.Forward.ToString());
                break;
            case ("Backward"):
                GameManager.GM.Backward = newKey;
                buttonText.text = GameManager.GM.Backward.ToString();
                PlayerPrefs.SetString("BackwardKey", GameManager.GM.Backward.ToString());
                break;
            case ("Left"):
                GameManager.GM.Left = newKey;
                buttonText.text = GameManager.GM.Left.ToString();
                PlayerPrefs.SetString("LeftKey", GameManager.GM.Left.ToString());
                break;
            case ("Right"):
                GameManager.GM.Right = newKey;
                buttonText.text = GameManager.GM.Right.ToString();
                PlayerPrefs.SetString("RightKey", GameManager.GM.Right.ToString());
                break;
        }
        yield return null;
    }
}
