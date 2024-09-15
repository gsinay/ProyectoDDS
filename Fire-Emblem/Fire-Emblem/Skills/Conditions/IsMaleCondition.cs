using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class IsMaleCondition(bool rivalGender = false) : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        if (rivalGender)
            return opponent.Info.Gender == "Male";
        return character.Info.Gender == "Male";
    }
}