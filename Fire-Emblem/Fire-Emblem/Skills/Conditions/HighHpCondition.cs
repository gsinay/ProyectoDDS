using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

public class HighHpCondition : ICondition
{
    private double _percentage;
    private bool _rivalHp;

    public HighHpCondition(double percentage, bool rivalHp = false)
    {
        _percentage = percentage;
        _rivalHp = rivalHp;
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        if (_rivalHp)
        {
            return opponent.GetRemainingHpPercentage() >= _percentage;
        }

        return character.GetRemainingHpPercentage() >= _percentage;
    }
}