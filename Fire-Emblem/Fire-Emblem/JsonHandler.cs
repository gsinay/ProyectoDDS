namespace Fire_Emblem;
using System.Text.Json;
using Fire_Emblem.Characters;

public class JsonHandler
{
    public Character GetCharacter(string name)
    {
        var characterData = FindCharacterData(name);
        return CreateCharacterFromData(characterData);
    }
    
    private Dictionary<string, string> FindCharacterData(string name)
    {
        var characterJsonList = ReadCharacterJson();
        var characterData = characterJsonList
            .FirstOrDefault(c => c["Name"].Equals(name, StringComparison.OrdinalIgnoreCase));

        if (characterData == null)
        {
            throw new ArgumentException($"Character '{name}' does not exist.");
        }

        return characterData;
    }


    private List<Dictionary<string, string>> ReadCharacterJson()
    {
        string jsonString = File.ReadAllText("characters.json");
        var characterJsonList = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonString);

        if (characterJsonList == null)
        {
            throw new InvalidOperationException("Error during deserialization: character list is null.");
        }

        return characterJsonList;
    }

   
    private Character CreateCharacterFromData(Dictionary<string, string> characterData)
    {
        return new Character
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
    }

    private static WeaponName GetWeaponName(string weaponString)
    {
        if (Enum.TryParse<WeaponName>(weaponString, true, out var weapon))
        {
            return weapon;
        }
        throw new ArgumentException($"Invalid weapon type: {weaponString}");
    }
}