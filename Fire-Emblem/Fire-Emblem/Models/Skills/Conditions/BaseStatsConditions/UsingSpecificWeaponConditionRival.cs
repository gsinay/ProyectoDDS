using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Skills.Conditions.BaseStatsConditions;

public class UsingSpecificWeaponConditionRival: ICondition
{
    private readonly WeaponName _weapon;
   

    public UsingSpecificWeaponConditionRival(WeaponName weapon)
    {
        _weapon = weapon;
       
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        return opponent.Info.Weapon == _weapon;
    }

}