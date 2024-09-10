using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class CompositeCondition : ICondition
{
    private readonly List<ICondition> _conditions;
    private readonly bool _useOr;

    public CompositeCondition(List<ICondition> conditions, bool useOr)
    {
        _conditions = conditions;
        _useOr = useOr;
    }

    public bool IsSatisfied(Character character, Character opponent)
    {
        if (_useOr)
        {
            return _conditions.Any(condition => condition.IsSatisfied(character, opponent));
        }
        else
        {
            return _conditions.All(condition => condition.IsSatisfied(character, opponent));
        }
    }
}