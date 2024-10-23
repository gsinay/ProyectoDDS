using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.BaseStatEffects;

public class StatPenaltyNeutralizeEffect : IEffect
{
    private readonly StatName _stat;

    public StatPenaltyNeutralizeEffect(StatName stat)
    {
        _stat = stat;
    }

    public void Apply(Character character, Character opponent)
    {
        character.Stats.NeutralizedPenalties.Neutralize(_stat);
    }
}