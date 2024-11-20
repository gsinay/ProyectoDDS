using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class BeforeCombatHpReductionEffect: IEffect
{

    private int _amountOfHpReduction;
    
    public BeforeCombatHpReductionEffect(int amountOfHpReduction)
    {
        _amountOfHpReduction = amountOfHpReduction;
    }
    public void Apply(Character character, Character opponent)
    {
        character.CharacterModifiers.CombatModifiers.BeforeCombatHpReduction += _amountOfHpReduction;
    }
}