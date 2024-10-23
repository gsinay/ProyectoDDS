using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class FollowupAttackPercentReductionEffect : IEffect
{
    private  double _reductionPercent;

    public FollowupAttackPercentReductionEffect(double reductionPercent)
    {
        _reductionPercent = reductionPercent;
    }

    public void Apply(Character character, Character opponent)
    {
       
        character.CharacterModifiers.FollowupModifiers.ReducePercentageOfDamageReceived(_reductionPercent);
    }
    
}