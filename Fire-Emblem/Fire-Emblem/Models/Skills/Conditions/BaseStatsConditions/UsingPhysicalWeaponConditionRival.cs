using Fire_Emblem.Characters;
namespace Fire_Emblem.Skills.Conditions;

public class UsingPhysicalWeaponConditionRival: ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return opponent.Info.Weapon == WeaponName.Axe ||
               opponent.Info.Weapon == WeaponName.Bow ||
               opponent.Info.Weapon == WeaponName.Lance ||
               opponent.Info.Weapon == WeaponName.Sword;
    }

}