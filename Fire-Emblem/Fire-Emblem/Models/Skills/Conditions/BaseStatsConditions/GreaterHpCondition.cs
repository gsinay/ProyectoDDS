using Fire_Emblem.Models.Characters;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;

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