using Fire_Emblem.Characters;
namespace Fire_Emblem;


public class InitiatingCombatCondition(bool rivalStarting = false) : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return rivalStarting ? opponent.IsInitiatingCombat : character.IsInitiatingCombat;
    }
}