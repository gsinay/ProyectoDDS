using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

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
        int baseStatValue = GetBaseStat(character); 
        int additionalBonus = baseStatValue / _scalingFactor; 

        if (additionalBonus > 0)
        {
            ApplyBonus(character, additionalBonus);
        }
    }

    private int GetBaseStat(Character character)
    {
        return _stat switch
        {
            StatName.Atk => character.Stats.BaseStats.GetBaseStat(StatName.Atk),
           StatName.Spd => character.Stats.BaseStats.GetBaseStat(StatName.Spd),
            StatName.Def => character.Stats.BaseStats.GetBaseStat(StatName.Def),
            StatName.Res => character.Stats.BaseStats.GetBaseStat(StatName.Res),
            _ => 0
        };
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