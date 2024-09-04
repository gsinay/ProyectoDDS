using Fire_Emblem.Characters;
namespace Fire_Emblem;


public class InitiatingCombatCondition : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return character.IsInitiatingCombat;
    }
}