using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class PercentDamageReductionPercentReductionEffect: IEffect
{
    
    private readonly double _reductionPercentage;
    
    public PercentDamageReductionPercentReductionEffect(double reductionPercentage)
    {
        _reductionPercentage = reductionPercentage;
    }
    public void Apply(Character character, Character opponent)
    {
        double currentReduction = 1 - character.CharacterModifiers.CombatModifiers.PercentDamageReceived;
        double reductionOfReduction = currentReduction * _reductionPercentage;
        character.CharacterModifiers.CombatModifiers.PercentDamageReceived += reductionOfReduction;
    }

   
}