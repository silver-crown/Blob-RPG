
using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// Dispatches an event every frame when a MonoBehaviour's coroutines are resumed
/// </summary>
/// <author>Jackson Dunstan, http://JacksonDunstan.com/articles/3382
/// <license>MIT</license>
public class CorutineUpdater : IUpdater
{
    private MonoBehaviour monoBehaviour;
    private Coroutine coroutine;

    /// <summary>
    /// Dispatched every frame
    /// </summary>
    public event Action OnUpdate;


    public MonoBehaviour MonoBehaviour {
        get { 
            return monoBehaviour; 
        }
        set {
            Stop();
            monoBehaviour = value;
            Start();
        }
    }
    /// <summary>
    /// Start dispatching OnUpdate every frame
    /// </summary>
    public void Start() {
        if(coroutine == null && monoBehaviour) {
            coroutine = monoBehaviour.StartCoroutine(DispatchOnUpdate());
        }
    }

    public void Stop() {
        if(coroutine != null && monoBehaviour) {
            monoBehaviour.StopCoroutine(coroutine);
        }
        coroutine = null;
    }
    private IEnumerator DispatchOnUpdate() {
        while (true) {
            if(OnUpdate != null) {
                OnUpdate();
            }
            yield return null;
        }
    }
}