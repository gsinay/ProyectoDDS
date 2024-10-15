namespace Fire_Emblem.Skills.Conditions;
using Characters;

public class OpposingWeaponTypeCondition : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        return IsPhysicalVsMagic(character, opponent) || IsMagicVsPhysical(character, opponent);
    }

    private bool IsPhysicalVsMagic(Character character, Character opponent)
    {
        return IsPhysicalWeapon(character.Info.Weapon) && IsMagicWeapon(opponent.Info.Weapon);
    }

    private bool IsMagicVsPhysical(Character character, Character opponent)
    {
        return IsMagicWeapon(character.Info.Weapon) && IsPhysicalWeapon(opponent.Info.Weapon);
    }

    private bool IsPhysicalWeapon(WeaponName weapon)
    {
        return weapon == WeaponName.Sword || 
               weapon == WeaponName.Axe || 
               weapon == WeaponName.Lance || 
               weapon == WeaponName.Bow;
    }

    private bool IsMagicWeapon(WeaponName weapon)
    {
        return weapon == WeaponName.Magic;
    }
}