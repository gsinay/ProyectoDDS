namespace Fire_Emblem;

public class SkillList
{
    private List<Skill> _skills;

    public SkillList()
    {
        _skills = new List<Skill>();
    }

    public void AddSkill(Skill skill)
    {
        _skills.Add(skill);
    }

    public int Count() => _skills.Count;

    public List<Skill> GetSkills() => _skills;

}