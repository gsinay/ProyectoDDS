namespace Fire_Emblem.Characters;

public class CharacterInfo
{
    public readonly string Name;
    public readonly WeaponName Weapon;
    public readonly string Gender;
    public readonly string DeathQuote;
    
    public CharacterInfo(string name, WeaponName weapon, string gender, string deathQuote)
    {
        Name = name;
        Weapon = weapon;
        Gender = gender;
        DeathQuote = deathQuote;
    }
}