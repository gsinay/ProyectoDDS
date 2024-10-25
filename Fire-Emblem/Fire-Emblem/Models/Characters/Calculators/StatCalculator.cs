using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Characters.Calculators;

public class StatCalculator
{
    public int GetGeneralEffectiveStat(Character character, StatName stat)
    {
        int effectiveBonuses = character.Stats.NeutralizedBonuses.IsNeutralized(stat)
            ? 0
            : character.Stats.CombatBonuses.GetBonus(stat);

        int effectivePenalties = character.Stats.NeutralizedPenalties.IsNeutralized(stat)
            ? 0
            : character.Stats.CombatPenalties.GetPenalty(stat);
        
        return Math.Max(0, character.Stats.BaseStats.GetBaseStat(stat) + effectiveBonuses - effectivePenalties);
    }
    public int GetEffectiveStat(Character character, StatName stat)
    {
        int effectiveBonuses = character.Stats.NeutralizedBonuses.IsNeutralized(stat)
            ? 0
            : character.Stats.CombatBonuses.GetBonus(stat) + character.Stats.FirstAttackBonuses.GetBonus(stat) +
              (character.HasAttacked ? character.Stats.FollowupBonuses.GetBonus(stat) : 0);

        int effectivePenalties = character.Stats.NeutralizedPenalties.IsNeutralized(stat)
            ? 0
            : character.Stats.CombatPenalties.GetPenalty(stat) + character.Stats.FirstAttackPenalties.GetPenalty(stat) +
              (character.HasAttacked ? character.Stats.FollowupPenalties.GetPenalty(stat) : 0);
            
        return Math.Max(0, character.Stats.BaseStats.GetBaseStat(stat) + effectiveBonuses - effectivePenalties);
    }
}