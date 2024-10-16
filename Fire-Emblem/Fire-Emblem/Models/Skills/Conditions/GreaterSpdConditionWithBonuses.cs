using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

public class GreaterSpdConditionWithBonuses : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
      
        return character.GeneralEffectiveSpd > opponent.GeneralEffectiveSpd;
    }
}