public class PopUp
{
    private const float DefaultTimer = 5;

    public string Message { get; set; }
    public float Duration { get; set; }

    public PopUp (string message)
    {
        Message = message;
        Duration = DefaultTimer;
    }

    public PopUp (string message, float timer) : this(message)
    {
        Duration = timer;
    }
}