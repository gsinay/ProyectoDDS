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
        int characterGeneralStat = character.GetGeneralStat(_characterStat);
        int rivalGeneralStat = opponent.GetGeneralStat(_rivalStat);
 
        return characterGeneralStat >= rivalGeneralStat + _threshold;
    }
    
}