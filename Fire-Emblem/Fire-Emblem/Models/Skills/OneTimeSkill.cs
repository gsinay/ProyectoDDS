using Fire_Emblem.Characters;
using Fire_Emblem.Collections;
using Fire_Emblem.Skills.Conditions;
using Fire_Emblem.Skills.Effects;

namespace Fire_Emblem.Skills;

public class OneTimeSkill : BaseSkill
{
    public OneTimeSkill(string name, string description, ICondition condition, EffectsList effects)
        : base(name, description, condition, effects)
    {
    }

}