using System.Collections;

/// <summary>
/// State for interacting with an object in front of the player, takes an object as an argument in the constructor parameter
/// </summary>
public class InteractState : State
{
    public InteractState() {
    }

    public override IEnumerator Start() {
        return base.Start();
    }

    public override IEnumerator End() {
        return base.End();
    }

    public override IEnumerator Execute() {
        return base.Execute();
    }

    public override IEnumerator Pause() {
        return base.Pause();
    }

    public override IEnumerator Resume() {
        return base.Resume();
    }

    public override IEnumerator StateUpdate() {
        return base.StateUpdate();
    }
}
