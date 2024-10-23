using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Skills.Conditions;

namespace Fire_Emblem.Skills.Conditions;

public class LowHpCondition : ICondition
{
    private readonly double _percentage;

    public LowHpCondition(double percentage)
    {
        _percentage = percentage;
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        return character.GetRemainingHpPercentage() <= _percentage;
    }
}