using Fire_Emblem.Models.Characters;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;

public class HighHpConditionRival : ICondition
{
    private readonly double _percentage;
    

    public HighHpConditionRival(double percentage)
    {
        _percentage = percentage;
        
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
      
        return opponent.GetRemainingHpPercentage() >= _percentage;
            
    }
}