using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Conditions;

public class GreaterResConditionWithBonuses : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return character.GeneralEffectiveRes > opponent.GeneralEffectiveRes;
    }
}