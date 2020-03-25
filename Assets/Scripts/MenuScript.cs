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
        menuPanel = transform.Find("Panel");
        menuPanel.gameObject.SetActive(false);
        waitingForKey = false;

        //iterate through each child, set the corresponding buttons to display the appropriate key
        SetupMenuButtonNames();
    }

    void Update(){
        if (Input.GetKeyDown(GameManager.GM.Pause) && !menuPanel.gameObject.activeSelf) {
            menuPanel.gameObject.SetActive(true);
        } else if(Input.GetKeyDown(GameManager.GM.Pause)){
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
                GameManager.GM.Upward = newKey;
                buttonText.text = GameManager.GM.Upward.ToString();
                PlayerPrefs.SetString("ForwardKey", GameManager.GM.Upward.ToString());
                break;
            case ("Backward"):
                GameManager.GM.Downward = newKey;
                buttonText.text = GameManager.GM.Downward.ToString();
                PlayerPrefs.SetString("BackwardKey", GameManager.GM.Downward.ToString());
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
            case ("Pause"):
                GameManager.GM.Pause = newKey;
                buttonText.text = GameManager.GM.Pause.ToString();
                PlayerPrefs.SetString("PauseKey", GameManager.GM.Pause.ToString());
                break;
        }
        yield return null;
    }

    /// <summary>
    /// Iterate through the list of names, if it matches, set the button name.
    /// </summary>
    /// <returns></returns>
    void SetupMenuButtonNames() {
        for (int i = 0; i < menuPanel.childCount; i++) {
            if (ButtonNames().Contains(menuPanel.GetChild(i).name)) {
                switch (menuPanel.GetChild(i).name) {
                    case "ForwardKey":
                        menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Upward.ToString();
                        break;
                    case "BackwardKey":
                        menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Downward.ToString();
                        break;
                    case "LeftKey":
                        menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Left.ToString();
                        break;
                    case "RightKey":
                        menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Right.ToString();
                        break;
                    case "PauseKey":
                        menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Pause.ToString();
                        break;
                }
            }
        }
    }
    //iterate through the list of names, it it matches. do the thing
    List<string> ButtonNames() {
        List<string> buttonNames = new List<string>();
        buttonNames.Add("ForwardKey");
        buttonNames.Add("BackwardKey");
        buttonNames.Add("LeftKey");
        buttonNames.Add("RightKey");
        buttonNames.Add("PauseKey");
        return buttonNames;
    }
}
