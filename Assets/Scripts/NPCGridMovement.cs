using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for NPC movement across a grid-by-grid system.
/// </summary>
public class NPCGridMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform movePoint;

    // Start is called before the first frame update
    void Start() {
        if (moveSpeed == 0) {
            moveSpeed = 2.0f;
        }
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    /// <summary>
    /// Function for moving across the grid in a linear fashion
    /// </summary>
    void Move() {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.0f) {
            if (Input.GetKey(GameManager.GM.Forward)) {
                movePoint.position += new Vector3(0.0f, 1.0f, 0.0f);
            }
            else if (Input.GetKey(GameManager.GM.Backward)) {
                movePoint.position += new Vector3(0.0f, -1.0f, 0.0f);
            }
            else if (Input.GetKey(GameManager.GM.Left)) {
                movePoint.position += new Vector3(-1.0f, 0.0f, 0.0f);
            }
            else if (Input.GetKey(GameManager.GM.Right)) {
                movePoint.position += new Vector3(1.0f, 0.0f, 0.0f);
            }
        }
    }
}
