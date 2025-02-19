using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Collections.Generic;
using System.Text;

public class VisualTextBlock : MonoBehaviour
{
    private const float AppearDuration = 1f;
    private const float DisappearDuration = 2f;

    private const float AppearInterpolationPower = 3f;
    private const float DisappearInterpolationPower = 0.3f;

    private const float MoveAmount = 50f;

    public IDisplay display;

    public TextMeshProUGUI text;

    private Vector2 offset;

    public VisualTextBlock()
    {
        //this.display = new();
        this.offset = Vector2.zero;
    }

    public void Start()
    {
        //this.displays = new();
    }

    public void RegisterDisplay(IDisplay display)
    {
        this.display = display;
    }

    private Vector2 GetOffsetFromTime(float time)
    {
        Vector2 offset;

        float duration = display.GetDuration();

        float disappearStartTime = duration - DisappearDuration;

        if (time > disappearStartTime)
        {
            float factor = (time - disappearStartTime) / DisappearDuration;
            float t = Mathf.Pow(factor, AppearInterpolationPower);

            offset = new Vector2(0, t * MoveAmount);

            this.gameObject.SetActive(time < duration);
        }
        else if (time < AppearDuration)
        {
            float factor = time;
            float t = Mathf.Pow(factor, DisappearInterpolationPower);

            offset = new Vector2(0, MoveAmount - t * MoveAmount);
        }
        else
        {
            offset = Vector2.zero;
        }

        return offset;
    }

    public void Update()
    {
        if (display == null) return;

        this.gameObject.GetComponent<RectTransform>().anchoredPosition = display.Transform().anchoredPosition + offset;

        offset = GetOffsetFromTime(display.GetTime());

        this.text.text = (string) display.GetContent();
    }
}