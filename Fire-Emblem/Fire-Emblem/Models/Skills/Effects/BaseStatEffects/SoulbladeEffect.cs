using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.BaseStatEffects;

public class SoulbladeEffect : IEffect
{
    public void Apply(Character? character, Character opponent)
    {
        int average = CalculateDefResAverage(opponent);
        AdjustDefense(opponent, average);
        AdjustResistance(opponent, average);
    }

    private int CalculateDefResAverage(Character opponent)
    {
        return (opponent.Stats.BaseStats.GetBaseStat(StatName.Def) + opponent.Stats.BaseStats.GetBaseStat(StatName.Res)) / 2;
    }

    private void AdjustDefense(Character opponent, int average)
    {
        int defDifference = average - opponent.Stats.BaseStats.GetBaseStat(StatName.Def);
        if (defDifference > 0)
        {
            opponent.Stats.CombatBonuses.AddBonus(StatName.Def, defDifference);
        }
        else
        {
            opponent.Stats.CombatPenalties.AddPenalty(StatName.Def, -1 *defDifference);
        }
    }

    private void AdjustResistance(Character opponent, int average)
    {
        int resDifference = average - opponent.Stats.BaseStats.GetBaseStat(StatName.Res);
        if (resDifference > 0)
        {
            opponent.Stats.CombatBonuses.AddBonus(StatName.Res, resDifference);
        }
        else
        {
            opponent.Stats.CombatPenalties.AddPenalty(StatName.Res, -1 * resDifference);
        }
    }
}