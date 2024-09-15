namespace Fire_Emblem;
using Characters;

public class OpposingWeaponTypeCondition : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        bool characterUsingPhysicalWeapon = IsPhysicalWeapon(character.Info.Weapon);
        bool opponentUsingMagicWeapon = opponent.Info.Weapon == "Magic";

        bool characterUsingMagicWeapon = character.Info.Weapon == "Magic";
        bool opponentUsingPhysicalWeapon = IsPhysicalWeapon(opponent.Info.Weapon);

        return (characterUsingPhysicalWeapon && opponentUsingMagicWeapon) ||
               (characterUsingMagicWeapon && opponentUsingPhysicalWeapon);
    }

    private bool IsPhysicalWeapon(string weapon)
    {
        return weapon == "Sword" || weapon == "Axe" || weapon == "Lance" || weapon == "Bow";
    }
}
