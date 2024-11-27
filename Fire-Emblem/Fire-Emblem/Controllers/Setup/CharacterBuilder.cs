using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Player;
using Fire_Emblem.Models.Skills;
using Fire_Emblem.SkillsManager;

namespace Fire_Emblem.Controllers.Setup;

public class CharacterBuilder
{
    private readonly JsonHandler _jsonHandler = new();
    private readonly SkillFactory _skillFactory = new();
    
    public Character CreateCharacter(string line)
    {
        int spaceIndex = line.IndexOf(' ');

        if (spaceIndex == -1)
        {
            return _jsonHandler.GetCharacter(line);
        }

        string name = line.Substring(0, spaceIndex);
        string skills = line.Substring(spaceIndex + 1);
        skills = skills.Trim('(', ')');
        string[] skillsArray = skills.Split(',');
        Character returnCharacter = _jsonHandler.GetCharacter(name);
        foreach (var skillName in skillsArray)
        {
            ISkill skill = _skillFactory.GetSkill(skillName);
            returnCharacter.AddSkill(skill);
        }

        return returnCharacter;
    }
}
