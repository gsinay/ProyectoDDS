using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class GreaterHpCondition : ICondition
{
    private int _hpDifference;

    public GreaterHpCondition(int hpDifference)
    {
        _hpDifference = hpDifference;
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        return character.GetHP >= opponent.GetHP + _hpDifference;
    }
}