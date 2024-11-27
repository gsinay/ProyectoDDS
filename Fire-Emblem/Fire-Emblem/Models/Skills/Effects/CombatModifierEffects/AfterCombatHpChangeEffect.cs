using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class AfterCombatHpChangeEffect: IEffect
{

    private int _amountOfHpIncrement;
    
    public AfterCombatHpChangeEffect(int amountOfHpIncrement)
    {
        _amountOfHpIncrement = amountOfHpIncrement;
    }
    public void Apply(Character character, Character opponent)
    {
        character.CharacterModifiers.CombatModifiers.AfterCombatHpChange += _amountOfHpIncrement;
    }
}