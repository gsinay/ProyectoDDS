using Fire_Emblem.Characters;
namespace Fire_Emblem.Skills.Conditions;

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