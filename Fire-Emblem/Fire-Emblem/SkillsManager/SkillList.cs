namespace Fire_Emblem;

public class SkillList
{
    private List<ISkill> _skills;

    public SkillList()
    {
        _skills = new List<ISkill>();
    }

    public void AddSkill(ISkill skill)
    {
        _skills.Add(skill);
    }

    public int Count() => _skills.Count;

    public List<ISkill> GetSkills() => _skills;

}