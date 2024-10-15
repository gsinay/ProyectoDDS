using Fire_Emblem.Characters;
using Fire_Emblem.Skills.Conditions;
using Fire_Emblem.Skills.Effects;

namespace Fire_Emblem.Skills;

public class Skill : BaseSkill
{
    public Skill(string name, string description, ICondition condition, List<IEffect> effects)
        : base(name, description, condition, effects)
    {
    }
}