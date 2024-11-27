using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Conditions.WithBonusesConditions;

public class DualStatComparisonCondition : ICondition
{
    private readonly StatName _statAttacker1;
    private readonly StatName _statAttacker2;
    private readonly StatName _statOpponent1;
    private readonly StatName _statOpponent2;
    
    private readonly int _threshold;
    
    public DualStatComparisonCondition(StatName statAttacker1, StatName statAttacker2, 
        StatName statOpponent1, StatName statOpponent2, int threshold = 0)
    {
        _statAttacker1 = statAttacker1;
        _statAttacker2 = statAttacker2;
        _statOpponent1 = statOpponent1;
        _statOpponent2 = statOpponent2;
        _threshold = threshold;
    }

    
    public bool IsSatisfied(Character character, Character opponent)
    {

        int attackerStat1 = character.GetGeneralStat(_statAttacker1);
        int attackerStat2 = character.GetGeneralStat(_statAttacker2);
        int opponentStat1 = opponent.GetGeneralStat(_statOpponent1);
        int opponentStat2 = opponent.GetGeneralStat(_statOpponent2);
        
        return attackerStat1 + attackerStat2 > opponentStat1 + opponentStat2 + _threshold;
    }
    
    
    
}