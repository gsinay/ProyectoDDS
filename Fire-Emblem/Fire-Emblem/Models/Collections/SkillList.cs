using Fire_Emblem.Skills;

namespace Fire_Emblem.SkillsManager;

public class SkillList
{
    private readonly List<ISkill> _skills;

    public SkillList()
    {
        _skills = new List<ISkill>();
    }

    public void AddSkill(ISkill skill)
    {
        _skills.Add(skill);
    }

    public void RemoveSkill(ISkill skill)
    {
        _skills.Remove(skill);
    }

    public int Count() => _skills.Count;

    public List<ISkill> GetSkills() => _skills;

}