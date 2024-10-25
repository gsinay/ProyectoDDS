using System.Collections;
using Fire_Emblem.Models.Skills;


namespace Fire_Emblem.Models.Collections;

public class SkillList : IEnumerable<ISkill>
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

    public IReadOnlyList<ISkill> GetSkills() => _skills;
    
    public IEnumerator<ISkill> GetEnumerator()
    {
        return _skills.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}