using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.BaseStatEffects;

public class LunaPenaltyEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        int defPenalty = opponent.Stats.BaseStats.GetBaseStat(StatName.Def) / 2;
        int resPenalty = opponent.Stats.BaseStats.GetBaseStat(StatName.Res) / 2;

        opponent.Stats.FirstAttackPenalties.AddPenalty(StatName.Def, defPenalty);
        opponent.Stats.FirstAttackPenalties.AddPenalty(StatName.Res, resPenalty);
    }
}