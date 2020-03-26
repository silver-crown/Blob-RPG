using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrontColliderCheck : MonoBehaviour
{
    public bool WalkingIntoWall;
    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Obstacle") {
            Debug.Log("collider moved into a wall");
            WalkingIntoWall = true;
        }
        else WalkingIntoWall = false;
    }
}
