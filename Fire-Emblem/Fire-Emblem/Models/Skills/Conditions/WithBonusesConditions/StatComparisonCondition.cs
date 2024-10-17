using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

public class StatComparisonCondition : ICondition
{

    private readonly StatName _characterStat;
    private readonly StatName _rivalStat;

    public StatComparisonCondition(StatName characterStat, StatName rivalStat)
    {
        _characterStat = characterStat;
        _rivalStat = rivalStat;
    }

    
    public bool IsSatisfied(Character character, Character opponent)
    {
        int characterGeneralStat = GetGeneralStat(character, _characterStat);
        int rivalGeneralStat = GetGeneralStat(opponent, _rivalStat);
 
        return characterGeneralStat > rivalGeneralStat;
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