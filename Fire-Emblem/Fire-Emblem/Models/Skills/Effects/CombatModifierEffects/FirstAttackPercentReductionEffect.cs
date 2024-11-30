using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class FirstAttackPercentReductionEffect : IEffect
{
    private  double _reductionPercent;

    public FirstAttackPercentReductionEffect(double reductionPercent)
    {
        _reductionPercent = reductionPercent;
    }

    public void Apply(Character character, Character opponent)
    {
       
        character.CharacterModifiers.FirstAttackModifiers.DamageReduction.AddEffect(_reductionPercent);
    }
    
}