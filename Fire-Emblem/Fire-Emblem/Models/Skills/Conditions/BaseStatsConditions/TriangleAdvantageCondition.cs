using Fire_Emblem.Characters;
using Fire_Emblem.Handlers;

namespace Fire_Emblem.Skills.Conditions;

public class TriangleAdvantageCondition : ICondition
{
    private readonly WtbHandler _wtbhandler = new();
    private readonly double _winnerValue = 1.2;
   
    public bool IsSatisfied(Character character, Character opponent)
    {
        return _wtbhandler.GetTriangleAdvantage(character, opponent) == _winnerValue;
    }
}