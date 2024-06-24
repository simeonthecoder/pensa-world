using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialText : MonoBehaviour
{
    public Text text;
    public trigger_with_player_tutorial collisionPlayer;
    public GameObject shiftTutorialPlacePos;
    public GameObject E_TutorialPlacePos;
    public GameObject DoorTutorialPlacePos;
    public GameObject chestOpen;
    public GameObject chestClosed;
    public GameObject tutorial_place;
    public float fadeDuration = 1.0f;

    private bool inside = false;
    private bool doneWASD = false;
    private bool doneE = false;
    private bool doneShift = false;
    private bool doneTurningCamera = false;
    private bool doneTutorial = false;

    private string[] dialog = {
        "Завъртете камерата с въртене на мишката или натискане на стрелките",
        "Можете да се движите с WASD или чрез натискане на стрелките",
        "Бягайте със Shift",
        "Натиснете Е за да взаимодействате с обект",
        "Отидете до вратата"
    };

    private void Start()
    {
        chestOpen.SetActive(false);
        tutorial_place.SetActive(false);
        text.text = dialog[0];
        StartCoroutine(FadeIn());
    }

    private void Update()
    {


        if ((Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0) && !doneTurningCamera)
        {
            StartCoroutine(ChangeTextWithFade(dialog[1]));
            doneTurningCamera = true;
            tutorial_place.SetActive(true);
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && (!doneWASD && doneTurningCamera) && (collisionPlayer.inside))
        {

            StartCoroutine(ChangeTextWithFade(dialog[2]));
            

            
            tutorial_place.transform.position = shiftTutorialPlacePos.transform.position;
            doneWASD = true;
            collisionPlayer.inside = false;

        }

        if ((Input.GetKey(KeyCode.LeftShift)) && !doneShift && doneWASD && (collisionPlayer.inside))
        {
            StartCoroutine(ChangeTextWithFade(dialog[3]));
            
            
            tutorial_place.transform.position = E_TutorialPlacePos.transform.position;
            doneShift = true;
        }

        if (Input.GetKey(KeyCode.E) && !doneE && doneShift && (collisionPlayer.inside))
        {
            StartCoroutine(ChangeTextWithFade(dialog[4]));
            chestOpen.SetActive(true);
            chestClosed.SetActive(false);
            tutorial_place.transform.position = DoorTutorialPlacePos.transform.position;
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
