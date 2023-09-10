using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OverworldPauseMenu : GameMenu
{
    public void StyleButtonPressed() {
        GameObject styleMenu = Resources.Load("StyleEditMenu", typeof(GameObject)) as GameObject;
        Open(styleMenu);
    }

}
