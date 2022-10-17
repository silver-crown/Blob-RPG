﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{

    [SerializeField] private Transform targetToFollow;
    public float min_X;
    public float max_X;
    public float min_Y;
    public float max_Y;

    void Update()
    {
        FollowPlayerCharacter();
        transform.position = new Vector3(Mathf.Clamp(targetToFollow.position.x, min_X, max_X),
                                        Mathf.Clamp(targetToFollow.position.y, min_Y, max_Y),
                                        transform.position.z);
    }

    void FollowPlayerCharacter(){
        foreach(GameObject p in CombatManager.CM.playerChars){
            if (p.GetComponent<player2DController>().playerChar){
                targetToFollow = p.transform;
            }
        }

    }
}
