
using System.Text.Json;

namespace Fire_Emblem;
public class JsonHandler
{
    
    public Character GetCharacter(string name)
    {
        string jsonString = File.ReadAllText("characters.json");
        var characterJsonList = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonString);

        if (characterJsonList == null)
        {
            throw new Exception("Error con la deserializacion");
        }

        var characterData = characterJsonList.FirstOrDefault(c =>
            c["Name"].Equals(name, StringComparison.OrdinalIgnoreCase));

        if (characterData == null)
        {
            throw new Exception($"No existe ese character");
        }

        var character = new Character
        {
            Name = characterData["Name"],
            Weapon = characterData["Weapon"],
            Gender = characterData["Gender"],
            DeathQuote = characterData["DeathQuote"],
            Atk = int.Parse(characterData["Atk"]),
            Spd = int.Parse(characterData["Spd"]),
            Def = int.Parse(characterData["Def"]),
            Res = int.Parse(characterData["Res"])
        };
        
        character.SetHP(int.Parse(characterData["HP"]));

        return character;
    }

    public Skill GetSkill(string skillName)
    {
        string jsonString = File.ReadAllText("skills.json");
        var skills = JsonSerializer.Deserialize<List<Skill>>(jsonString);

        if (skills == null)
        {
            throw new Exception("Error en deserializar");
        }

        var skill = skills.FirstOrDefault(s => s.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));

        if (skill == null)
        {
            throw new Exception($"No se encontró la skill de ese nombre");
        }

        return skill;
    }
}

