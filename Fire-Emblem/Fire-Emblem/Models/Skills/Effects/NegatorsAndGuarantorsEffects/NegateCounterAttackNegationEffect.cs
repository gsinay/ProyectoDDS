using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.NegatorsAndGuarantorsEffects;

public class NegateCounterAttackNegationEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        character.CharacterModifiers.CombatModifiers.NegatedCounterAttackNegation = true;
    }
}