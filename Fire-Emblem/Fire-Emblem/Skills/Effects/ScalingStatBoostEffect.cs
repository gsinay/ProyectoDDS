using Fire_Emblem;
using Fire_Emblem.Characters;

public class ScalingStatBoostEffect : IEffect
{
    private readonly string _stat; 
    private readonly int _scalingFactor; 

    public ScalingStatBoostEffect(string stat, int scalingFactor)
    {
        _stat = stat;
        _scalingFactor = scalingFactor;
    }

    public void Apply(Character character, Character opponent, CombatLog combatLog)
    {
        int baseStatValue = GetBaseStat(character); 
        int additionalBonus = baseStatValue / _scalingFactor; 

        if (additionalBonus > 0)
        {
            ApplyBonus(character, additionalBonus);
            combatLog.LogBonus(character, _stat, additionalBonus);
        }
    }

    private int GetBaseStat(Character character)
    {
        return _stat switch
        {
            "Atk" => character.Atk,
            "Spd" => character.Spd,
            "Def" => character.Def,
            "Res" => character.Res,
            _ => 0
        };
    }

    private void ApplyBonus(Character character, int amount)
    {
        switch (_stat)
        {
            case "Atk":
                character.AtkModifier += amount;
                break;
            case "Spd":
                character.SpdModifier += amount;
                break;
            case "Def":
                character.DefModifier += amount;
                break;
            case "Res":
                character.ResModifier += amount;
                break;
        }
    }
}
