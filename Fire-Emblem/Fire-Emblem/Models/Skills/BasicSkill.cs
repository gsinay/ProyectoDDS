using Fire_Emblem.Collections;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Skills;

public class BasicSkill : BaseSkill
{
    public BasicSkill(string name, string description, ICondition condition, EffectsList effects)
        : base(name, description, condition, effects)
    {
    }
}