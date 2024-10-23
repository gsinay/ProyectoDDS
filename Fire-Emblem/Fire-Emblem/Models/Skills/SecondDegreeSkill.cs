using Fire_Emblem.Models.Collections;
using Fire_Emblem.Models.Skills.Conditions;

namespace Fire_Emblem.Models.Skills;

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