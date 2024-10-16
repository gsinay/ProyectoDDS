using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

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