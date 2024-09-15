using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class OneTimeCondition : ICondition
{
    private bool _used = false;

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