using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OverworldPauseMenu : GameMenu
{
    //destroy current menu (will ideally have maybe sort of a fade here)
    public override void Close() {
        Destroy(gameObject);
    }

    //opens a new menu
    public override void Open(GameObject m) {
        Instantiate(m);
        Close();
    }

    public void StyleButtonPressed() {
        GameObject styleMenu = Resources.Load("StyleEditMenu", typeof(GameObject)) as GameObject;
        Open(styleMenu);
    }
}
