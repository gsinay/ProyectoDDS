using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

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