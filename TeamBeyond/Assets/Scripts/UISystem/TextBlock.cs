using System.Diagnostics;
using UnityEngine;

public class TextBlock : IDisplay
{
    private string text;
    private RectTransform transform;

    private float time;
    private bool timeTicking;

    public float Duration { get; set; }

    public TextBlock ()
    {
        this.text = string.Empty;
        this.transform = new RectTransform();

        this.Refresh(0);
    }

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateContent(string message)
    {
        this.text = message;
    }

    public void Refresh (float deltaTime)
    {
        if (timeTicking) this.time += deltaTime;
    }

    public void SetTransform(RectTransform transform)
    {
        this.transform = transform;
        this.Refresh(0);
    }

    public RectTransform Transform()
    {
        return this.transform;
    }

    public object GetContent()
    {
        return this.text;
    }

    public void ResumeTime()
    {
        this.timeTicking = true;
    }

    //This is the same type of stand as Star Platinum
    public void PauseTime()
    {
        this.timeTicking = false;
    }

    public float GetTime()
    {
        return this.time;
    }

    public void SetTime(float time)
    {
        this.time = time;
    }

    public float GetDuration()
    {
        return this.Duration;
    }
}