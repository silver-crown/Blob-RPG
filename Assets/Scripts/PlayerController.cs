using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script taking care of the very generalized specifications of the player
/// The pause menu, enemy, dialogue box, etc.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] public MenuScript PauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
