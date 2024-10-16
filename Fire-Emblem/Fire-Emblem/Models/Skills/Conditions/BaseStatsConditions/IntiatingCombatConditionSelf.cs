using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;


public class InitiatingCombatConditionSelf : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return character.IsInitiatingCombat;
    }
}