using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class NegateCounterAttackEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        opponent.CharacterModifiers.CombatModifiers.CounterAttackIsNegated = true;
    }
}