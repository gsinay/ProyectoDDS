namespace Fire_Emblem;

public class Skill
{
    private string _name;
    private string _description;

    public Skill(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public string Name => _name;

    public string Description => _description;
}