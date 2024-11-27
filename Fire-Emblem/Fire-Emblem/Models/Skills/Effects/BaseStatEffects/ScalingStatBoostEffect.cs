using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.BaseStatEffects;

public class ScalingStatBoostEffect : IEffect
{
    private readonly StatName _stat; 
    private readonly int _scalingFactor; 

    public ScalingStatBoostEffect(StatName stat, int scalingFactor)
    {
        _stat = stat;
        _scalingFactor = scalingFactor;
    }

    public void Apply(Character character, Character opponent)
    {
        int baseStatValue = character.Stats.BaseStats.GetBaseStat(_stat);
        int additionalBonus = baseStatValue / _scalingFactor; 

        if (additionalBonus > 0)
        {
            ApplyBonus(character, additionalBonus);
        }
    }
    

    private void ApplyBonus(Character character, int amount)
    {
        switch (_stat)
        {
            case StatName.Atk:
                character.Stats.CombatBonuses.AddBonus(StatName.Atk, amount);
                break;
            case StatName.Spd:
                character.Stats.CombatBonuses.AddBonus(StatName.Spd, amount);
                break;
            case StatName.Def:
                character.Stats.CombatBonuses.AddBonus(StatName.Def, amount);
                break;
            case StatName.Res:
                character.Stats.CombatBonuses.AddBonus(StatName.Res, amount); 
                break;
        }
    }
}