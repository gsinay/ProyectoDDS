using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.BaseStatEffects;

public class StatPenaltyPercentageOpponentEffect : IEffect
{
    private readonly StatName _stat;
    private readonly double _percentage;

    public StatPenaltyPercentageOpponentEffect(StatName stat, double percentage)
    {
        _stat = stat;
        _percentage = percentage;
    }


    public void Apply(Character character, Character opponent)
    {
        int baseAmount = opponent.Stats.BaseStats.GetBaseStat(_stat);
        int amount = (int) (baseAmount * _percentage);
        opponent.Stats.CombatPenalties.AddPenalty(_stat, amount);
    }
}