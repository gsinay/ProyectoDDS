namespace Fire_Emblem.Characters;

public class CharacterInfo
{
    public string Name;
    public string Weapon;
    public string Gender;
    public string DeathQuote;
    
    public CharacterInfo(string name, string weapon, string gender, string deathQuote)
    {
        Name = name;
        Weapon = weapon;
        Gender = gender;
        DeathQuote = deathQuote;
    }
}