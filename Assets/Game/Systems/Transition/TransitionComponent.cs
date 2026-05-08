
public class TransitionInProgress
{
    public string To;

    public float Time;
    public float Duration = 1.5f;

    public TransitionInProgress(string toState)
    {
        To = toState;
    }
}

public struct TransitionReady
{
    public string To;

    public TransitionReady(string toState)
    {
        To = toState;
    }
}
