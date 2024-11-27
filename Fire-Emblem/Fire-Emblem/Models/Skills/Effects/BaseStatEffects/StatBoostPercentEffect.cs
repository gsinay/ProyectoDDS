using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.BaseStatEffects;

public class StatBoostPercentEffect : IEffect
{
    private readonly StatName _stat;
    private readonly StatName _comparisonStat;
    private readonly double _percent;

    public StatBoostPercentEffect(StatName stat, StatName comparisonStat, double percent)
    {
        _stat = stat;
        _comparisonStat = comparisonStat;
        _percent = percent;
    }
    
    public void Apply(Character character, Character opponent)
    {
        int baseAmount = character.Stats.BaseStats.GetBaseStat(_comparisonStat);
        int amount = (int) (baseAmount * _percent);
        character.Stats.CombatBonuses.AddBonus(_stat, amount);
    }
}