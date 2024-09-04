using Fire_Emblem_View;
using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class Setup
{

    private View _view;
    private string _teamsFolder;
    private JsonHandler _jsonHandler = new JsonHandler();
    private SkillFactory _skillFactory;
    private Player[] _players;

    public Player[] Players => _players;
    
    public Setup(View view, string teamsFolder)
    {
        _view = view;
        _skillFactory = new SkillFactory(_view);
        _teamsFolder = teamsFolder;
        _players = new Player[2];
        _players[0] = new Player(playerNumber:1);
        _players[1] = new Player(playerNumber:2);


    }

    public void SetUpGame()
    {
        _view.WriteLine($"Elige un archivo para cargar los equipos");
        ListFiles();
        string selectedFileName = FormatFileName(Convert.ToInt32(_view.ReadLine()));
        SetUpPlayers(selectedFileName);


    }

    private void ListFiles()
    {
        string[] files = GetOrderedFiles();

        if (files.Length == 0)
        {
            return;
        }

        DisplayFiles(files);
    }

    private string[] GetOrderedFiles()
    {
        if (!Directory.Exists(_teamsFolder)) return [];
        string[] files = Directory.GetFiles(_teamsFolder);
        Array.Sort(files);
        return files;
    }

    private void DisplayFiles(string[] files)
    {
        for (var i = 0; i < files.Length; i++)
        {
            var fileName = Path.GetFileName(files[i]);
            _view.WriteLine($"{i}: {fileName}");
        }
    }

    private string FormatFileName(int selectedFileIndex)
    {
        string[] files = GetOrderedFiles();
        var fileName = Path.GetFileName(files[selectedFileIndex]);
        return Path.Combine(_teamsFolder, fileName);
    }

    private void SetUpPlayers(string fileName)
    {
        string[] lines = File.ReadAllLines(fileName);
        int currentTeam = 0;
        for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
        {
            string line = lines[lineNumber];
            if (line == "Player 1 Team")
            {
                currentTeam = 0;
            }

            else if (line == "Player 2 Team")
            {
                currentTeam = 1;
            }
            else
            {
                Character character = CreateCharacter(line);
                _players[currentTeam].AddCharacter(character);
            }

        }
    }

    private Character CreateCharacter(string line)
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
        for (int skillIndex = 0; skillIndex < skillsArray.Length; skillIndex++)
        {
            ISkill skill = _skillFactory.GetSkill(skillsArray[skillIndex]);
            returnCharacter.AddSkill(skill);
        }

        return returnCharacter;
    }

    public bool IsValidTeams()
    {
        foreach (var player in _players)
        {
            if (player.CharacterCount() is < 1 or > 3)
                return false;
            
            var characterNames = player.Characters.Select(c => c.Name).ToList();
            if (characterNames.Count != characterNames.Distinct().Count())
                return false;

            foreach (var character in player.Characters)
            {
                if (character.SkillCount() > 2)
                    return false;

                var skillNames = character.GetSkills().Select(s => s.Name).ToList();
                if (skillNames.Count != skillNames.Distinct().Count())
                    return false;
            }
        }

        return true;
    }

}


