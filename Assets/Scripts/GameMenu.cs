using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GameMenu : MonoBehaviour
{

    //Should open a new menu
    public abstract void Open(GameObject m);
    //Closes this menu
    public abstract void Close();
}
