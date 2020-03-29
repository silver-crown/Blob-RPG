using System;
/// <summary>
/// Periodically dispatches an event
/// </summary>
/// <author>Jackson Dunstan, http://JacksonDunstan.com/articles/3382
/// <license>MIT</license>
public interface IUpdater
{


    event Action OnUpdate;

    /// <summary>
    /// Start OnUpdate events events
    /// </summary>
    void Start();
    /// <summary>
    /// Stop OnUpdate events
    /// </summary>
    void Stop();
}
