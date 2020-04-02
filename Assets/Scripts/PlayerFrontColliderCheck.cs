using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrontColliderCheck : MonoBehaviour
{
    int mask;
    public bool WalkingIntoWall;
    public bool WalkingIntoInteractable;

    private void Update() {
        mask = LayerMask.GetMask("Obstacle");
        if (Physics2D.OverlapCircle(transform.position, 0.2f, mask)) {
            Debug.Log("Front Collider overlapping");
            WalkingIntoWall = true;
        }
        else {
            WalkingIntoWall = false;
        }
    }
}
