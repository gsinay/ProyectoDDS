using Fire_Emblem.Characters;


public interface ICondition
{
    bool IsSatisfied(Character character, Character opponent);

}