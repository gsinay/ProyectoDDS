namespace Fire_Emblem.Skills.Conditions;
using Characters;

public class OpposingWeaponTypeCondition : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        bool characterUsingPhysicalWeapon = IsPhysicalWeapon(character.Info.Weapon);
        bool opponentUsingMagicWeapon = opponent.Info.Weapon == WeaponName.Magic;

        bool characterUsingMagicWeapon = character.Info.Weapon == WeaponName.Magic;
        bool opponentUsingPhysicalWeapon = IsPhysicalWeapon(opponent.Info.Weapon);

        return (characterUsingPhysicalWeapon && opponentUsingMagicWeapon) ||
               (characterUsingMagicWeapon && opponentUsingPhysicalWeapon);
    }

    private bool IsPhysicalWeapon(WeaponName weapon)
    {
        return weapon == WeaponName.Sword || 
               weapon == WeaponName.Axe || 
               weapon == WeaponName.Lance || 
               weapon == WeaponName.Bow;
    }
}
