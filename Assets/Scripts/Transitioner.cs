using PixelCrushers.SceneStreamer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitioner : MonoBehaviour
{
    [SerializeField] String sceneToTransitionTo;
    public string target;
    public string AreaName;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            TransitionManager.TM.transitionSpot = target;
            StartCoroutine(TransitionManager.TM.LoadLevel(sceneToTransitionTo));
        }
    }
}
