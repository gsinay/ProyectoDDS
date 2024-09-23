using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class SoulbladeEffect : IEffect
{
    public void Apply(Character? character, Character opponent)
    {
        var average = (opponent.Stats.BaseStats[StatName.Def] + opponent.Stats.BaseStats[StatName.Res]) / 2;

        int defDifference = average - opponent.Stats.BaseStats[StatName.Def];
        int resDifference = average - opponent.Stats.BaseStats[StatName.Res];

        if (defDifference > 0)
            opponent.Stats.CombatBonuses[StatName.Def] += defDifference;
        else
            opponent.Stats.CombatPenalties[StatName.Def] += defDifference;
        
        if (resDifference > 0)
            opponent.Stats.CombatBonuses[StatName.Res] += resDifference;
        else
            opponent.Stats.CombatPenalties[StatName.Res] += resDifference;
    }
}