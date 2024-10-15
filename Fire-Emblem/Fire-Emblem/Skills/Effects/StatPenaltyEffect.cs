using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class StatPenaltyEffect : IEffect
{
    private readonly StatName _stat;
    private readonly int _amount;

    public StatPenaltyEffect(StatName stat, int amount)
    {
        _stat = stat;
        _amount = amount;
    }
    
    public void Apply(Character character, Character opponent)
    {
        character.Stats.CombatPenalties.AddPenalty(_stat,_amount);
    }
}