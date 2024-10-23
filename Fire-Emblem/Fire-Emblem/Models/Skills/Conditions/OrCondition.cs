using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Collections;

namespace Fire_Emblem.Models.Skills.Conditions;

public class OrCondition : ICondition
{
    private readonly ConditionsList _conditions;

    public OrCondition(ConditionsList conditions)
    {
        _conditions = conditions;
    }

    public bool IsSatisfied(Character character, Character opponent)
    {
        foreach (var condition in _conditions.GetConditions())
        {
            if (condition.IsSatisfied(character, opponent))
            {
                return true;
            }
        }
        return false;
    }
}