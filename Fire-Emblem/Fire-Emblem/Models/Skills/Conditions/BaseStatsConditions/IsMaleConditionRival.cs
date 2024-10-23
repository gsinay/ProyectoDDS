using Fire_Emblem.Models.Characters;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;

public class IsMaleConditionRival : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return opponent.Info.Gender == "Male";
    }
}