using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected bool executingMethod;
    /// <summary>
    /// Beginning the state
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator Start() {
        yield break;
    }
    /// <summary>
    /// Executing the contents of the state
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator Execute() {
        yield break;
    }
    /// <summary>
    /// Ending the state
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator End() {
        yield break;
    }
    /// <summary>
    /// Temporarily transitioning into another state
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator Pause() {
        yield break;
    }
    /// <summary>
    /// Resuming to the state after pause
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator Resume() {
        yield break;
    } 
}
