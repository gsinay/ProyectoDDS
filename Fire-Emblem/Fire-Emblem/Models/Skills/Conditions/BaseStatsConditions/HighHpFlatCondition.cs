using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;

public class HighHpFlatCondition: ICondition
{
    private readonly double _amount;
    

    public HighHpFlatCondition(double amount)
    {
        _amount = amount;
        
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        int remainingHp = character.Stats.BaseStats.GetBaseStat(StatName.Hp);
        return remainingHp >= _amount;

    }
}