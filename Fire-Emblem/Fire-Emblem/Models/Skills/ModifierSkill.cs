using Fire_Emblem.Collections;
using Fire_Emblem.Models.Skills;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Skills;

public class ModifierSkill : BaseSkill
{
    public ModifierSkill(string name, string description, ICondition condition, EffectsList effects)
        : base(name, description, condition, effects)
    {
    }
    protected ModifierSkill(string name, string description)
        : base(name, description)
    {
    }
    
    public ModifierSkill(ICondition condition, EffectsList effects)
        : base(condition, effects)
    {
    }
    
    
}