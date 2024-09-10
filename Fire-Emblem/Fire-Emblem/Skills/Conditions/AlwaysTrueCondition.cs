using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class AlwaysTrueCondition: ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return true;
    }
}
