using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

public class GreaterHpCondition : ICondition
{
    private readonly int _hpDifference;

    public GreaterHpCondition(int hpDifference)
    {
        _hpDifference = hpDifference;
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        return character.GetHp>= opponent.GetHp + _hpDifference;
    }
}