public class UIQuest
{
    private string name;
    private string description;

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    public string Description
    {
        get { return this.description; }
        set { this.description = value; }
    }

    public UIQuest(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    public override string ToString()
    {
        return $"{this.Name}\n{this.Description}";
    }
}
