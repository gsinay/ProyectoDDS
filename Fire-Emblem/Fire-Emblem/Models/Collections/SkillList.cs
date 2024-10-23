using Fire_Emblem.Models.Skills;
using Fire_Emblem.Skills;

namespace Fire_Emblem.Models.Collections;

public class SkillList
{
    private readonly List<ISkill> _skills;

    public SkillList(IEnumerable<ISkill> skills)
    {
        _skills = new List<ISkill>(skills);
    }
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