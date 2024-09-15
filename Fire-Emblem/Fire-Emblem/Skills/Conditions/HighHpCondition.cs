using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class HighHpCondition : ICondition
{
    private double _percentage;
    private bool _rivalHP;

    public HighHpCondition(double percentage, bool rivalHp = false)
    {
        _percentage = percentage;
        _rivalHP = rivalHp;
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        if (_rivalHP)
        {
            return opponent.GetRemainingHpPercentage() >= _percentage;
        }

        return character.GetRemainingHpPercentage() >= _percentage;
    }
}