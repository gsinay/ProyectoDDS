namespace Fire_Emblem;
using Characters;

public class OpposingWeaponTypeCondition : ICondition
{
    public bool IsSatisfied(Character character, Character opponent)
    {
        bool characterUsingPhysicalWeapon = IsPhysicalWeapon(character.Weapon);
        bool opponentUsingMagicWeapon = opponent.Weapon == "Magic";

        bool characterUsingMagicWeapon = character.Weapon == "Magic";
        bool opponentUsingPhysicalWeapon = IsPhysicalWeapon(opponent.Weapon);

        return (characterUsingPhysicalWeapon && opponentUsingMagicWeapon) ||
               (characterUsingMagicWeapon && opponentUsingPhysicalWeapon);
    }

    private bool IsPhysicalWeapon(string weapon)
    {
        return weapon == "Sword" || weapon == "Axe" || weapon == "Lance" || weapon == "Bow";
    }
}
