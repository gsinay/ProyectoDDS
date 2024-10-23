using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Conditions;

public interface ICondition
{
    bool IsSatisfied(Character character, Character opponent);

}