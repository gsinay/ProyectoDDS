using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class FlatDamageReductionEffect : IEffect
{
    private readonly int _amountReduced;

    public FlatDamageReductionEffect(int amountReduced)
    {
        _amountReduced = amountReduced;
    }

    public void Apply(Character character, Character opponent)
    {
        character.CharacterModifiers.CombatModifiers.FlatDamageReduction += _amountReduced;
    }
}