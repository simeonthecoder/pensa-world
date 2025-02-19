using UnityEngine;

public interface IDisplay
{
    void Refresh(float deltaTime);

    RectTransform Transform();

    void SetTransform (RectTransform transform);

    void UpdateContent (string message);

    object GetContent();

    void Destroy();

    void ResumeTime();

    void PauseTime();

    void SetTime (float time);

    float GetTime();

    float GetDuration();
}