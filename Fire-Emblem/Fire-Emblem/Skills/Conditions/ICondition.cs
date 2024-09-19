using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

public interface ICondition
{
    bool IsSatisfied(Character character, Character opponent);

}