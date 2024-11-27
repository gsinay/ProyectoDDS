using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.BaseStatEffects;

public class ScalingPenaltyEffect : IEffect
{
    private int _maxReduction;
    private readonly StatName _statUsedForComparison;
    private readonly StatName _affectedStat;

    private readonly double _percentage;


    public ScalingPenaltyEffect(StatName statUsedForComparison, StatName affectedStat,
        double percentage, int maxReduction)
    {
        _statUsedForComparison = statUsedForComparison;
        _affectedStat = affectedStat;
        _maxReduction = maxReduction;
        _percentage = percentage;
    }

    public  void Apply(Character character, Character opponent)
    {
        int characterStat = GetStatValue(character);
        int rivalStat = GetStatValue(opponent);
        
        int statDifference = characterStat - rivalStat;
        
        int reduction = Math.Min((int)(statDifference * _percentage), _maxReduction);
        
        opponent.Stats.CombatPenalties.AddPenalty(_affectedStat, reduction);

    }

    private int GetStatValue(Character character)
    {
        return character.Stats.BaseStats.GetBaseStat(_statUsedForComparison);
    }
}