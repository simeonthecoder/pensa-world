using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UIQuestSystem
{
    private Dictionary<UIQuest, IDisplay> quests;

    public UIQuestSystem()
    {
        this.quests = new();
    }

    public void AddQuest (UIQuest quest)
    {
        this.quests.Add(quest, new TextBlock());
    }

    public void RemoveQuest (UIQuest quest)
    {
        this.quests.Remove(quest);
    }

    public Dictionary<UIQuest, IDisplay> GetQuests()
    {
        return this.quests;
    }
}