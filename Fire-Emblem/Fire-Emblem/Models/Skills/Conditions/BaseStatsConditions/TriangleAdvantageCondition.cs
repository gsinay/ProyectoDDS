using Fire_Emblem.Controllers.Handlers;
using Fire_Emblem.Models.Characters;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;

public class TriangleAdvantageCondition : ICondition
{
    private readonly WtbHandler _wtbhandler = new();
    private readonly double _winnerValue = 1.2;
   
    public bool IsSatisfied(Character character, Character opponent)
    {
        return _wtbhandler.GetTriangleAdvantage(character, opponent) == _winnerValue;
    }
}