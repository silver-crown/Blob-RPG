using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    /// <summary>
    /// function for moving the character around
    /// </summary>
    void Move() {

        if (Input.GetKey(GameManager.GM.Forward)) {
            transform.position += Vector3.up * Time.deltaTime;
        }
        else if(Input.GetKey(GameManager.GM.Backward)) {
            transform.position += Vector3.down * Time.deltaTime;
        }
        else if (Input.GetKey(GameManager.GM.Left)) {
            transform.position += Vector3.left * Time.deltaTime;
        }
        else if (Input.GetKey(GameManager.GM.Right)) {
            transform.position += Vector3.right * Time.deltaTime;
        }
    }
}
