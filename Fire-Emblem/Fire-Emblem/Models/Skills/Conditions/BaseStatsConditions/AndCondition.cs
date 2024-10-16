using Fire_Emblem.Characters;
using Fire_Emblem.Collections;

namespace Fire_Emblem.Skills.Conditions;

public class AndCondition : ICondition
{
    private readonly ConditionsList _conditions;

    public AndCondition(ConditionsList conditions)
    {
        _conditions = conditions;
    }

    public bool IsSatisfied(Character character, Character opponent)
    {
        foreach (var condition in _conditions.GetConditions())
        {
            if (!condition.IsSatisfied(character, opponent))
            {
                return false;
            }
        }
        return true;
    }
}