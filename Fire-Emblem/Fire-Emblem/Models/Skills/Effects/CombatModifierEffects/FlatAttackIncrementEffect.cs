using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class FlatAttackIncrementEffect : IEffect
{
    private readonly int _amountIncremented;

    public FlatAttackIncrementEffect(int amountIncremented)
    {
        _amountIncremented = amountIncremented;
    }

    public void Apply(Character character, Character opponent)
    {
        character.CharacterModifiers.CombatModifiers.FlatAttackIncrement += _amountIncremented;
    }
}