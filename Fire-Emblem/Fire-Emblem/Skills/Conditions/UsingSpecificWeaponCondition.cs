
using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class UsingSpecificWeaponCondition: ICondition
{
    private string _weapon;

    public UsingSpecificWeaponCondition(string weapon)
    {
        _weapon = weapon;
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        return character.Weapon == _weapon;
    }

}