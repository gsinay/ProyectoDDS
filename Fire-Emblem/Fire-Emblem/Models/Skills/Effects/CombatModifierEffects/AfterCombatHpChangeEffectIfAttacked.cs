using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class AfterCombatHpChangeEffectIfAttacked: IEffect
{

    private int _amountOfHpIncrement;
    
    public AfterCombatHpChangeEffectIfAttacked(int amountOfHpIncrement)
    {
        _amountOfHpIncrement = amountOfHpIncrement;
    }
    public void Apply(Character character, Character opponent)
    {
        character.CharacterModifiers.CombatModifiers.AfterCombatIfAttackedHpChange += _amountOfHpIncrement;
    }
}