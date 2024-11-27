using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;

public class BaseStatComparisonCondition : ICondition
{

    private readonly StatName _characterStat;
    private readonly StatName _rivalStat;
    private readonly int _threshold;

    public BaseStatComparisonCondition(StatName characterStat, StatName rivalStat, int threshold = 0)
    {
        _characterStat = characterStat;
        _rivalStat = rivalStat;
        _threshold = threshold;
    }
    
    public bool IsSatisfied(Character character, Character opponent)
    {
        int charachterBaseStat = character.Stats.BaseStats.GetBaseStat(_characterStat);
        int rivalBaseStat = opponent.Stats.BaseStats.GetBaseStat(_rivalStat); 
        
        return charachterBaseStat >= rivalBaseStat + _threshold;
    }
    
}