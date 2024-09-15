using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class StatBonusNeutralizeEffect : IEffect
{
    private StatName _stat;

    public StatBonusNeutralizeEffect(StatName stat)
    {
        _stat = stat;
    }

    public void Apply(Character character, Character opponent)
    {
        opponent.Stats.NeutralizedBonuses[_stat] = true;
    }
}