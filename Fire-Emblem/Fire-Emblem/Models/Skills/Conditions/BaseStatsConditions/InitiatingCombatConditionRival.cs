using Fire_Emblem.Models.Characters;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;


public class InitiatingCombatConditionRival : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return opponent.IsInitiatingCombat;
    }
}