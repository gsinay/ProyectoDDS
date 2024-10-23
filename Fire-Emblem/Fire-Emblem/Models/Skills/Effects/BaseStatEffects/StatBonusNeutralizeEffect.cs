using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.BaseStatEffects;

public class StatBonusNeutralizeEffect : IEffect
{
    private readonly StatName _stat;

    public StatBonusNeutralizeEffect(StatName stat)
    {
        _stat = stat;
    }

    public void Apply(Character character, Character opponent)
    {
        opponent.Stats.NeutralizedBonuses.Neutralize(_stat);
    }
}