using System.Collections.Generic;

public class UISystem
{
    private UIQuestSystem questManager;

    private PopUp popUp;
    public TextBlock popUpTextBlock;

    public UISystem()
    {
        this.questManager = new();

        this.popUpTextBlock = new TextBlock();
    }

    public void Update(float deltaTime)
    {
        foreach (var quest in questManager.GetQuests())
        {
            quest.Value.UpdateContent(quest.Key.ToString());
            quest.Value.Refresh(deltaTime);
        }

        this.popUpTextBlock.UpdateContent(popUp.Message);
        this.popUpTextBlock.Refresh(deltaTime);
    }

    public void AddQuest (UIQuest quest)
    {
        this.questManager.AddQuest(quest);
    }

    public void RemoveQuest (UIQuest quest)
    {
        this.questManager.RemoveQuest(quest);
    }

    public Dictionary<UIQuest, IDisplay> GetQuests ()
    {
        return this.questManager.GetQuests();
    }

    public void UpdatePopUp (PopUp popUp)
    {
        this.popUpTextBlock.SetTime(0);
        this.popUpTextBlock.Duration = popUp.Duration;

        this.popUp = popUp;
    }
}