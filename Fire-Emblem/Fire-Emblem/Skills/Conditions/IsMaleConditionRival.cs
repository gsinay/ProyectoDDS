using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

public class IsMaleConditionRival : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return opponent.Info.Gender == "Male";
    }
}