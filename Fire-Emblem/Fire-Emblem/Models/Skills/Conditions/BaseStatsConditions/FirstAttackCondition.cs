using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

public class FirstAttackCondition : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return !character.HasAttacked;
    }
}
