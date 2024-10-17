using Fire_Emblem.Collections;
using Fire_Emblem.Models.Skills;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Skills;

public class ThirdDegreeSkill : BaseSkill
{
    public ThirdDegreeSkill(string name, string description, ICondition condition, EffectsList effects)
        : base(name, description, condition, effects)
    {
    }
    public ThirdDegreeSkill(ICondition condition, EffectsList effects)
        : base(condition, effects)
    {
    }
    
    
}