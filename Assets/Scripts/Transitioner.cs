using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitioner : MonoBehaviour
{
    [SerializeField] String sceneToTransitionTo;
    public string transitionSpot;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            TransitionManager.LL.transitionSpot = transitionSpot;
            StartCoroutine(TransitionManager.LL.LoadLevel(sceneToTransitionTo));
        }
    }
}
