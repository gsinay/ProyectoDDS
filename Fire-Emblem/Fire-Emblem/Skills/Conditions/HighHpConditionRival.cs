using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

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