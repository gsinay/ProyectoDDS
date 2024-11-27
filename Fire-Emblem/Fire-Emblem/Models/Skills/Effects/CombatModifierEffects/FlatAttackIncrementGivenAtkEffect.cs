using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class FlatAttackIncrementGivenAtkEffect : IEffect
{
    private readonly double _percent;
    private bool _consideringOpponent;

    public FlatAttackIncrementGivenAtkEffect(double percent, bool consideringOpponent = true)
    {
        _percent = percent;
        _consideringOpponent = consideringOpponent;
    }

    public void Apply(Character character, Character opponent)
    {
        double amountOfAtk;
        if (_consideringOpponent) 
             amountOfAtk = opponent.GeneralEffectiveAtk * _percent;
        else
            amountOfAtk = character.GeneralEffectiveAtk * _percent;
        character.CharacterModifiers.CombatModifiers.FlatAttackIncrement += (int)amountOfAtk;
    }
}