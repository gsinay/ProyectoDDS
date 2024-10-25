using System.Collections;
using Fire_Emblem.Models.Skills.Conditions;

namespace Fire_Emblem.Models.Collections;

public class ConditionsList : IEnumerable<ICondition>
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
    
    public IEnumerator<ICondition> GetEnumerator()
    {
        return _conditions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}