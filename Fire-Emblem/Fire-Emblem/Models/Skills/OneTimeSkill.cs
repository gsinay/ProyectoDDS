using Fire_Emblem.Models.Collections;
using Fire_Emblem.Models.Skills.Conditions;

namespace Fire_Emblem.Models.Skills;

public class OneTimeSkill : BaseSkill
{
    public OneTimeSkill(string name, string description, ICondition condition, EffectsList effects)
        : base(name, description, condition, effects)
    {
    }

}