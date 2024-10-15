using Fire_Emblem.Characters;
namespace Fire_Emblem.Skills.Conditions;

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