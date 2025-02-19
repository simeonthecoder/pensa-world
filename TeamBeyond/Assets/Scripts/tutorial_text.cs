using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialText : MonoBehaviour
{
    public UIManager uiManager;
    private UIQuest quest;

    public trigger_with_player_tutorial collisionPlayer;
    public GameObject shiftTutorialPlacePos;
    public GameObject E_TutorialPlacePos;
    public GameObject DoorTutorialPlacePos;
    public GameObject chestOpen;
    public GameObject chestClosed;
    public GameObject tutorial_place;

    private bool inside = false;
    private bool doneWASD = false;
    private bool doneE = false;
    private bool doneShift = false;
    private bool doneTurningCamera = false;
    private bool doneTutorial = false;

    private string[] dialog = {
        "Завъртете камерата",
        "Отидете до маркера",
        "Бягайте със Shift",
        "Отворете сандъка",
        "Продължете по пътеката"
    };

    private string[] descriptions = {
        "Mouse",
        "W,A,S,D",
        "Shift",
        "E",
        ""
    };

    private void Start()
    {
        this.quest = new(dialog[0], descriptions[0]);

        chestOpen.SetActive(false);
        tutorial_place.SetActive(false);

        uiManager.AddQuest(this.quest);
    }

    private void Update()
    {
        if ((Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0) && !doneTurningCamera)
        {
            ChangeText(dialog[1], descriptions[1]);

            doneTurningCamera = true;
            tutorial_place.SetActive(true);
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && (!doneWASD && doneTurningCamera) && (collisionPlayer.inside))
        {
            ChangeText(dialog[2], descriptions[2]);

            tutorial_place.transform.position = shiftTutorialPlacePos.transform.position;
            doneWASD = true;
            collisionPlayer.inside = false;
        }

        if ((Input.GetKey(KeyCode.LeftShift)) && !doneShift && doneWASD && (collisionPlayer.inside))
        {
            ChangeText(dialog[3], descriptions[3]);
            
            tutorial_place.transform.position = E_TutorialPlacePos.transform.position;
            doneShift = true;
        }

        if (Input.GetKey(KeyCode.E) && !doneE && doneShift && (collisionPlayer.inside))
        {
            ChangeText(dialog[4], descriptions[4]);
            chestOpen.SetActive(true);
            chestClosed.SetActive(false);
            tutorial_place.transform.position = DoorTutorialPlacePos.transform.position;
            doneE = true;
            
        }
    }

    private void ChangeText(string newText, string newDescription)
    {
        this.quest.Name = newText;
        this.quest.Description = newDescription;
    }
}
