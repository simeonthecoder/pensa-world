using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialText : MonoBehaviour
{
    public Text text;
    public float fadeDuration = 1.0f;
    private bool doneWASD = false;
    private bool doneE = false;
    private bool doneShift = false;
    private bool doneTurningCamera = false;

    private string[] dialog = {
        "Завъртете камерата с въртене на мишката или натискане на стрелките",
        "Можете да се движите с WASD или чрез натискане на стрелките",
        "Бягайте със Shift",
        "Натиснете Е за да взаимодействате с обект",
        "Отидете до вратата"
    };

    private void Start()
    {
        text.text = dialog[0];
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        if ((Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0) && !doneTurningCamera)
        {
            StartCoroutine(ChangeTextWithFade(dialog[1]));
            doneTurningCamera = true;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && !doneWASD && doneTurningCamera)
        {
            StartCoroutine(ChangeTextWithFade(dialog[2]));
            doneWASD = true;
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift)) && !doneShift && doneWASD)
        {
            StartCoroutine(ChangeTextWithFade(dialog[3]));
            doneShift = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && !doneE && doneShift)
        {
            StartCoroutine(ChangeTextWithFade(dialog[4]));
            doneE = true;
        }

    }

    private IEnumerator ChangeTextWithFade(string newText)
    {
        yield return StartCoroutine(FadeOut());
        text.text = newText;
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - normalizedTime);
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0); // ensure the text is fully transparent at the end
    }

    private IEnumerator FadeIn()
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            text.color = new Color(text.color.r, text.color.g, text.color.b, normalizedTime);
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1); // ensure the text is fully opaque at the end
    }
}
