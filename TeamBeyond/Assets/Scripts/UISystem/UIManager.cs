using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UISystem system;

    private VisualTextBlock popUpBlock;
    private VisualTextBlock currBlock;

    private int timer;

    public GameObject[] uiElements;
    public GameObject[] positions;

    private List<UIQuest> quests;

    public GameObject canvas;

    public UIManager()
    {
        this.quests = new();
        this.system = new();
    }

    public void AddQuest(UIQuest quest)
    {
        quests.Add(quest);
        system.AddQuest(quest);

        VisualTextBlock currTextBlock = Instantiate(uiElements[0]).GetComponent<VisualTextBlock>();

        TextBlock questDisplay = (TextBlock) system.GetQuests().Last().Value;

        questDisplay.SetTransform(positions[0].GetComponent<RectTransform>());
        questDisplay.Duration = 5;

        currTextBlock.RegisterDisplay(questDisplay);
        currTextBlock.transform.SetParent(canvas.transform);

        this.currBlock = currTextBlock;
    }

    public void AddQuest(UIQuest quest, Vector2 offset)
    {
        quests.Add(quest);
        system.AddQuest(quest);

        VisualTextBlock currTextBlock = Instantiate(uiElements[0]).GetComponent<VisualTextBlock>();

        TextBlock questDisplay = (TextBlock)system.GetQuests().Last().Value;

        RectTransform og = positions[0].GetComponent<RectTransform>();

        RectTransform pos = Instantiate(og, og.parent);
        pos.anchoredPosition = og.anchoredPosition;
        pos.sizeDelta = og.sizeDelta;
        pos.anchorMin = og.anchorMin;
        pos.anchorMax = og.anchorMax;
        pos.pivot = og.pivot;
        pos.rotation = og.rotation;
        pos.localScale = og.localScale;
        //pos.anchoredPosition = positions[0].GetComponent<RectTransform>().anchoredPosition;
        pos.anchoredPosition += offset;

        questDisplay.SetTransform(pos);
        questDisplay.Duration = 5;

        currTextBlock.RegisterDisplay(questDisplay);
        currTextBlock.transform.SetParent(canvas.transform);
    }

    public void RemoveQuest(UIQuest quest)
    {
        quests.Remove(quest);
        system.RemoveQuest(quest);

        Destroy(this.currBlock.gameObject);
        this.currBlock = null;
    }

    public void SetPopUp (PopUp popUp)
    {
        popUpBlock.gameObject.SetActive(true);
        system.UpdatePopUp(popUp);
    }

    public void Start()
    {
        timer = 0;

        this.popUpBlock = Instantiate(uiElements[1]).GetComponent<VisualTextBlock>();
        this.popUpBlock.RegisterDisplay(system.popUpTextBlock);

        this.popUpBlock.display.SetTransform(positions[1].GetComponent<RectTransform>());
        this.popUpBlock.display.ResumeTime();

        this.popUpBlock.transform.SetParent(canvas.transform);
    }

    public void Update()
    {
        system.Update(Time.deltaTime);
    }

    public UIQuest GetQuest(int id)
    {
        return quests[id];
    }
}