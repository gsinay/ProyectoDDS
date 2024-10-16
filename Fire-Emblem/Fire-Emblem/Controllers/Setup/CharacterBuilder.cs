using Fire_Emblem.Characters;
using Fire_Emblem.Skills;
using Fire_Emblem.SkillsManager;

namespace Fire_Emblem;

public class CharacterBuilder
{
    private JsonHandler _jsonHandler;
    private SkillFactory _skillFactory;

    public CharacterBuilder(JsonHandler jsonHandler, SkillFactory skillFactory)
    {
        _jsonHandler = jsonHandler;
        _skillFactory = skillFactory;
    }

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
