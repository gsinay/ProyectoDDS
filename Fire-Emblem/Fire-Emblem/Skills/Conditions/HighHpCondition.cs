using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class HighHpCondition : ICondition
{
    private double _percentage;

    public HighHpCondition(double percentage)
    {
        _percentage = percentage;
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        return character.GetRemainingHpPercentage() >= _percentage;
    }
}