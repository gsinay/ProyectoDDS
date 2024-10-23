using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;

public class UsingSpecificWeaponConditionSelf: ICondition
{
    private readonly WeaponName _weapon;
   

    public UsingSpecificWeaponConditionSelf(WeaponName weapon)
    {
        _weapon = weapon;
       
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        return character.Info.Weapon == _weapon;
    }

}