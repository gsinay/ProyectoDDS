using Fire_Emblem.Characters;
namespace Fire_Emblem.Skills.Conditions;

public class UsingPhysicalWeaponConditionSelf: ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return character.Info.Weapon == WeaponName.Axe ||
               character.Info.Weapon == WeaponName.Bow ||
               character.Info.Weapon == WeaponName.Lance ||
               character.Info.Weapon == WeaponName.Sword;
    }

}