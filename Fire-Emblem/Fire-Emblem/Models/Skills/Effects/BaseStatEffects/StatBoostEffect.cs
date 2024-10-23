using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.BaseStatEffects;

public class StatBoostEffect : IEffect
{
    private readonly StatName _stat;
    private readonly int _amount;

    public StatBoostEffect(StatName stat, int amount)
    {
        _stat = stat;
        _amount = amount;
    }
    
    public void Apply(Character character, Character opponent)
    {
        character.Stats.CombatBonuses.AddBonus(_stat, _amount);
    }
}