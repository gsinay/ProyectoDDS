using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

public class LowHpCondition : ICondition
{
    private double _percentage;

    public LowHpCondition(double percentage)
    {
        _percentage = percentage;
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        return character.GetRemainingHpPercentage() <= _percentage;
    }
}