namespace Fire_Emblem;

using Fire_Emblem.Characters;

public class FirstAttackCondition : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return !character.HasAttacked;
    }
}
