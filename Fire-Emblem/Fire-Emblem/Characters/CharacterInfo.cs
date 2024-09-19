namespace Fire_Emblem.Characters;

public class CharacterInfo
{
    public string Name;
    public WeaponName Weapon;
    public string Gender;
    public string DeathQuote;
    
    public CharacterInfo(string name, WeaponName weapon, string gender, string deathQuote)
    {
        Name = name;
        Weapon = weapon;
        Gender = gender;
        DeathQuote = deathQuote;
    }
}