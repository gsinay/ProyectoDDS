using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

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