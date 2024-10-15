using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class StatBoostOpponentEffect : IEffect
{
    private readonly StatName _stat;
    private readonly int _amount;

    public StatBoostOpponentEffect(StatName stat, int amount)
    {
        _stat = stat;
        _amount = amount;
    }

    public void Apply(Character character, Character opponent)
    {
        opponent.Stats.CombatBonuses.AddBonus(_stat, _amount);
    }
}