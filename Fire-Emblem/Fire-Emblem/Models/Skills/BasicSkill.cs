using Fire_Emblem.Models.Collections;
using Fire_Emblem.Models.Skills.Conditions;

namespace Fire_Emblem.Models.Skills;

public class BasicSkill : BaseSkill
{
    public BasicSkill(string name, string description, ICondition condition, EffectsList effects)
        : base(name, description, condition, effects)
    {
    }
    
    public BasicSkill(ICondition condition, EffectsList effects)
        : base(condition, effects)
    {
    }
}