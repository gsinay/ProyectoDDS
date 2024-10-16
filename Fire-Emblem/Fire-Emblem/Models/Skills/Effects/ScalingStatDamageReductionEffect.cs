using Fire_Emblem.Characters;
using Fire_Emblem.Skills.Effects;

public class ScalingStatDamageReductionEffect : IEffect
{
    private int _maxReductionPercentage;
    private readonly StatName _stat;

    public ScalingStatDamageReductionEffect(StatName stat, int maxReductionPercentage)
    {
        _stat = stat;
        _maxReductionPercentage = maxReductionPercentage;
    }

    public  void Apply(Character character, Character opponent)
    {
        int attackerStat = GetStatDifference(character);
        int defenderStat = GetStatDifference(opponent);


        int statDifference = attackerStat - defenderStat;
        
        int reduction = Math.Min(statDifference * 4, _maxReductionPercentage);
        double reductionPercentage = reduction / 100.0;
        reductionPercentage = Math.Max(0, reductionPercentage);

        character.CharacterModifiers.CombatModifiers.ReducePercentageOfDamageReceived(reductionPercentage);
    }

    private int GetStatDifference(Character character)
    {
        if (_stat == StatName.Atk)
            return character.GeneralEffectiveAtk;
        if (_stat == StatName.Def)
            return character.GeneralEffectiveDef;
        if (_stat == StatName.Spd)
            return character.GeneralEffectiveSpd;
        return character.GeneralEffectiveRes;
    }
}