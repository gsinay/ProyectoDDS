using Fire_Emblem.Models.Characters;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;

public class OneTimeCondition : ICondition
{
    private bool _used;

    public bool IsSatisfied(Character character, Character opponent)
    {
        if (!_used)
        {
            _used = true;  
            return true;
        }
        return false;
    }
}