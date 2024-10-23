using Fire_Emblem.Models.Skills.Conditions;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Collections;

public class ConditionsList
{
    private List<ICondition> _conditions;
    
    public ConditionsList(IEnumerable<ICondition> effects)
    {
        _conditions = new List<ICondition>(effects);
    }
    
    public IReadOnlyList<ICondition> GetConditions()
    {
        return _conditions.AsReadOnly();
    }
}