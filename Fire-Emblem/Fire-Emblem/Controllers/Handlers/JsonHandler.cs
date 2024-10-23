using System.Text.Json;
using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Exceptions;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Controllers;

public class JsonHandler
{
    public Character GetCharacter(string name)
    {
        var character = FindCharacter(name);
        return character;
    }
    
    private Character FindCharacter(string name)
    {
        var characters = ReadCharactersFromJson();
        var character = characters
            .FirstOrDefault(c => c.Info.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (character == null)
        {
            throw new CharacterDoesntExistException(name);
        }

        return character;
    }

    private List<Character> ReadCharactersFromJson()
    {
        string jsonString = File.ReadAllText("characters.json");
        var characterDataList = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonString);

        if (characterDataList == null)
        {
            throw new NullCharacterListException();
        }

        var characters = characterDataList.Select(data => CreateCharacterFromData(data)).ToList();

        return characters;
    }

    private Character CreateCharacterFromData(Dictionary<string, string> data)
    {
        return new Character
        (
            data["Name"],
            weapon: GetWeaponName(data["Weapon"]),
            data["Gender"],
            data["DeathQuote"],
            int.Parse(data["HP"]),
            int.Parse(data["Atk"]),
            int.Parse(data["Spd"]),
            int.Parse(data["Def"]),
            int.Parse(data["Res"])
        );
    }

    private static WeaponName GetWeaponName(string weaponString)
    {
        if (Enum.TryParse<WeaponName>(weaponString, true, out var weapon))
        {
            return weapon;
        }
        throw new InvalidWeaponTypeException(weaponString);
    }
}