using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Skills.Conditions.WithBonusesConditions;

public class StatComparisonCondition : ICondition
{

    private readonly StatName _characterStat;
    private readonly StatName _rivalStat;
    private readonly int _threshold;

    public StatComparisonCondition(StatName characterStat, StatName rivalStat, int threshold = 0)
    {
        _characterStat = characterStat;
        _rivalStat = rivalStat;
        _threshold = threshold;
    }

    
    public bool IsSatisfied(Character character, Character opponent)
    {
        int characterGeneralStat = GetGeneralStat(character, _characterStat);
        int rivalGeneralStat = GetGeneralStat(opponent, _rivalStat);
 
        return characterGeneralStat > rivalGeneralStat + _threshold;
    }

    private static int GetGeneralStat(Character unit, StatName stat)
    {
        if (stat == StatName.Atk)
            return unit.GeneralEffectiveAtk;
        if (stat == StatName.Def)
            return unit.GeneralEffectiveDef;
        if (stat == StatName.Res)
            return unit.GeneralEffectiveRes;
        return unit.GeneralEffectiveSpd;

    }
}