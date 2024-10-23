using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.BaseStatEffects;

public class FirstAttackBoostEffect : IEffect
{
    private readonly int _percentage;

    public FirstAttackBoostEffect(int percentage)
    {
        _percentage = percentage;
    }

    public void Apply(Character character, Character opponent)
    {
        int boostAmount = (character.Stats.BaseStats.GetBaseStat(StatName.Atk) * _percentage) / 100;
        character.Stats.FirstAttackBonuses.AddBonus(StatName.Atk, boostAmount);
        
    }
    
}
