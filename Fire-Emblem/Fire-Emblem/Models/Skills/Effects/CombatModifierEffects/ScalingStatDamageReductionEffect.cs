using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class ScalingStatDamageReductionEffect : IEffect
{
    private int _maxReductionPercentage;
    private readonly StatName _stat;
    private readonly int _multiplier;
    private readonly string _attackType;

    public ScalingStatDamageReductionEffect(StatName stat, int maxReductionPercentage, int multiplier = 4, 
        string attackType = "combat")
    {
        _stat = stat;
        _maxReductionPercentage = maxReductionPercentage;
        _multiplier = multiplier;
        _attackType = attackType;
    }

    public  void Apply(Character character, Character opponent)
    {
        int attackerStat = character.GetGeneralStat(_stat);
        int defenderStat = opponent.GetGeneralStat(_stat);


        int statDifference = attackerStat - defenderStat;
        
        int reduction = Math.Min(statDifference * _multiplier, _maxReductionPercentage);
        double reductionPercentage = reduction / 100.0;
        reductionPercentage = Math.Max(0, reductionPercentage);
        
        if (_attackType == "combat")
            character.CharacterModifiers.CombatModifiers.ReducePercentageOfDamageReceived(reductionPercentage);
        else if (_attackType == "first")
            character.CharacterModifiers.FirstAttackModifiers.ReducePercentageOfDamageReceived(reductionPercentage);
        else
            character.CharacterModifiers.FollowupModifiers.ReducePercentageOfDamageReceived(reductionPercentage);



    }

   
}