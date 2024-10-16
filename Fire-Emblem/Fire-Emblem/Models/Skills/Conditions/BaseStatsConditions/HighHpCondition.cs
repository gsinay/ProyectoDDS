using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

public class HighHpCondition: ICondition
{
    private readonly double _percentage;
    

    public HighHpCondition(double percentage)
    {
        _percentage = percentage;
        
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        int remainingHp = character.Stats.BaseStats.GetBaseStat(StatName.Hp);
        int maxHp = character.Stats.BaseStats.GetBaseStat(StatName.MaxHp);
        return Math.Round((double)remainingHp / maxHp, 2) >= _percentage;

    }
}