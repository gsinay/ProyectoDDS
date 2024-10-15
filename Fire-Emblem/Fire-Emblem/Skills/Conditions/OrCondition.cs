using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

public class OrCondition : ICondition
{
    private readonly List<ICondition> _conditions;

    public OrCondition(List<ICondition> conditions)
    {
        _conditions = conditions;
    }

    public bool IsSatisfied(Character character, Character opponent)
    {
        foreach (var condition in _conditions)
        {
            if (condition.IsSatisfied(character, opponent))
            {
                return true;
            }
        }
        return false;
    }
}