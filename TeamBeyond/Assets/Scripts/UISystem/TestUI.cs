using UnityEngine;

public class TestUI : MonoBehaviour
{
    private UIManager uiManager;

    private int counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = GameObject.Find("UI_MANAGER").GetComponent<UIManager>();

        uiManager.AddQuest(new UIQuest("Press V", "Press V 10 times!"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            counter++;
        }

        uiManager.GetQuest(0).Description = "Press V 10 times: " + counter + "/10";

        if (counter == 10)
        {
            uiManager.SetPopUp(new PopUp("Quest complete: Press V 10 times"));
            counter++;

            uiManager.AddQuest(new UIQuest("Test", "Yet another quest"));
        }
    }
}
