using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class FirstAttackPercentReductionEffect : IEffect
{
    private  double _reductionPercent;

    public FirstAttackPercentReductionEffect(double reductionPercent)
    {
        _reductionPercent = reductionPercent;
    }

    public void Apply(Character character, Character opponent)
    {
       
        character.CharacterModifiers.FirstAttackModifiers.ReducePercentageOfDamageRecieved(_reductionPercent);
    }
    
}