using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class AfterCombatHpReductionEffect: IEffect
{

    private int _amountOfHpReduction;
    
    public AfterCombatHpReductionEffect(int amountOfHpReduction)
    {
        _amountOfHpReduction = amountOfHpReduction;
    }
    public void Apply(Character character, Character opponent)
    {
        character.CharacterModifiers.CombatModifiers.AfterCombatHpChange -= _amountOfHpReduction;
    }
}