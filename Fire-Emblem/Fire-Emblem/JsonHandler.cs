
using System.Text.Json;
using Fire_Emblem.Characters;

namespace Fire_Emblem;
public class JsonHandler
{
    
    public Character GetCharacter(string name)
    {
        string jsonString = File.ReadAllText("characters.json");
        var characterJsonList = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonString);

        if (characterJsonList == null)
        {
            throw new Exception("Error during deserialization");
        }

        var characterData = characterJsonList.FirstOrDefault(c =>
            c["Name"].Equals(name, StringComparison.OrdinalIgnoreCase));

        if (characterData == null)
        {
            throw new Exception($"Character '{name}' does not exist.");
        }
        var character = new Character
        (
            characterData["Name"],
            weapon: GetWeaponName(characterData["Weapon"]),
            characterData["Gender"],
            characterData["DeathQuote"],
            int.Parse(characterData["HP"]),
            int.Parse(characterData["Atk"]),
            int.Parse(characterData["Spd"]),
            int.Parse(characterData["Def"]),
            int.Parse(characterData["Res"])
        );
        return character;
    }
    
    
    private static WeaponName GetWeaponName(string weaponString)
    {
        try
        {
            return (WeaponName)Enum.Parse(typeof(WeaponName), weaponString, true);
        }
        catch (ArgumentException)
        {
            throw new Exception($"Invalid weapon type: {weaponString}");
        }
    }



    
}

