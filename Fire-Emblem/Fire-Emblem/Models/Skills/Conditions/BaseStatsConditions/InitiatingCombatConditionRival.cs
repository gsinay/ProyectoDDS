using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;


public class InitiatingCombatConditionRival : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return opponent.IsInitiatingCombat;
    }
}