using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class ScalingStatBoostEffect : IEffect
{
    private readonly string _stat; 
    private readonly int _scalingFactor; 

    public ScalingStatBoostEffect(string stat, int scalingFactor)
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
            "Atk" => character.Stats.BaseStats["Atk"],
            "Spd" => character.Stats.BaseStats["Spd"],
            "Def" => character.Stats.BaseStats["Def"],
            "Res" => character.Stats.BaseStats["Res"],
            _ => 0
        };
    }

    private void ApplyBonus(Character character, int amount)
    {
        switch (_stat)
        {
            case "Atk":
                character.Stats.CombatBonuses["Atk"] += amount;
                break;
            case "Spd":
                character.Stats.CombatBonuses["Spd"] += amount;
                break;
            case "Def":
                character.Stats.CombatBonuses["Def"] += amount;
                break;
            case "Res":
                character.Stats.CombatBonuses["Res"] += amount;
                break;
        }
    }
}