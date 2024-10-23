using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;

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