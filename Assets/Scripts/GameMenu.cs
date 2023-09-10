using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GameMenu : MonoBehaviour
{
    //destroy current menu (will ideally have maybe sort of a fade here)
    public virtual void Close() {
        Destroy(gameObject);
    }

    //opens a new menu
    public virtual void Open(GameObject m) {
        Instantiate(m);
        Close();
    }
}
