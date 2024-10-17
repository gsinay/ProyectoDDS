using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

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