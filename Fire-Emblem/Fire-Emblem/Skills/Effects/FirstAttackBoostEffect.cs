using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class FirstAttackBoostEffect : IEffect
{
    private readonly int _percentage;

    public FirstAttackBoostEffect(int percentage)
    {
        _percentage = percentage;
    }

    public void Apply(Character character, Character opponent)
    {
        int boostAmount = (character.Stats.BaseStats[StatName.Atk] * _percentage) / 100;
        character.Stats.FirstAttackBonuses[StatName.Atk] += boostAmount;
        
    }
    
}
