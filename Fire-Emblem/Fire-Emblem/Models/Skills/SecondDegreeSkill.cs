using Fire_Emblem.Collections;
using Fire_Emblem.Models.Skills;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Skills;

public class SecondDegreeSkill : BaseSkill
{
    public SecondDegreeSkill(string name, string description, ICondition condition, EffectsList effects)
        : base(name, description, condition, effects)
    {
    }
    public SecondDegreeSkill(ICondition condition, EffectsList effects)
        : base(condition, effects)
    {
    }
    
    
}