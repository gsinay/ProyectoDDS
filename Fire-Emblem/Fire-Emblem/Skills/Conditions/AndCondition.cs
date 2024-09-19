using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

public class AndCondition : ICondition
{
    private readonly List<ICondition> _conditions;
    

    public AndCondition(List<ICondition> conditions)
    {
        _conditions = conditions;
     
    }

    public bool IsSatisfied(Character character, Character opponent)
    { 
        return _conditions.All(condition => condition.IsSatisfied(character, opponent));
    }
}