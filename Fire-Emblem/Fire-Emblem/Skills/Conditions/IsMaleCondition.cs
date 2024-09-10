using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class IsMaleCondition(bool rivalGender = false) : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        if (rivalGender)
            return opponent.Gender == "Male";
        return character.Gender == "Male";
    }
}