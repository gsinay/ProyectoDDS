namespace Fire_Emblem.Skills.Conditions;

using Fire_Emblem.Characters;

public class SameOpponentCondition : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return character.MostRecentOpponent == opponent;
    }
}
