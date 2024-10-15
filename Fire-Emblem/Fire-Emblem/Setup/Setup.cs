using Fire_Emblem.Exceptions;
using Fire_Emblem_View;
using Fire_Emblem.Characters;
using Fire_Emblem.SkillsManager;

namespace Fire_Emblem;

public class Setup
{
    private View _view;
    private TeamFileLoader _fileLoader;
    private CharacterBuilder _characterBuilder;
    private Player[] _players;

    public Player[] Players => _players;

    public Setup(View view, string teamsFolder, JsonHandler jsonHandler, SkillFactory skillFactory)
    {
        _view = view;
        _fileLoader = new TeamFileLoader(teamsFolder);
        _characterBuilder = new CharacterBuilder(jsonHandler, skillFactory);
        _players = new Player[2];
        _players[0] = new Player(playerNumber: 1);
        _players[1] = new Player(playerNumber: 2);
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
        string[] files = _fileLoader.GetOrderedFiles();

        if (files.Length == 0)
        {
            _view.WriteLine("No se encontraron archivos de equipos.");
            return;
        }

        DisplayFiles(files);
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
        string[] files = _fileLoader.GetOrderedFiles();
        var fileName = Path.GetFileName(files[selectedFileIndex]);
        return fileName;
    }

    public void SetUpPlayers(string fileName)
    {
        string[] lines = _fileLoader.LoadTeamFromFile(fileName);
        int currentTeam = 0;
        foreach (var line in lines)
        {
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
                Character character = _characterBuilder.CreateCharacter(line);
                _players[currentTeam].AddCharacter(character);
            }
        }
    }


    public bool AreTeamsValid()
    {
        return IsTeamValid(_players[0]) && IsTeamValid(_players[1]);
    }

    public bool IsTeamValid(Player player)
    {
        return IsCharacterCountValid(player) && AreCharacterNamesUnique(player) && IsSkillCountValid(player);
    }

    private bool IsCharacterCountValid(Player player)
    {
        return player.CharacterCount() is >= 1 and <= 3;
    }

    private bool AreCharacterNamesUnique(Player player)
    {
        var characterNames = player.Characters.Select(c => c.Info.Name).ToList();
        return characterNames.Count == characterNames.Distinct().Count();
    }

    private bool IsSkillCountValid(Player player)
    {
        foreach (var character in player.Characters)
        {
            if (character.SkillCount() > 2 || !AreSkillNamesUnique(character))
                return false;
        }
        return true;
    }

    private bool AreSkillNamesUnique(Character character)
    {
        var skillNames = character.GetSkills().Select(s => s.Name).ToList();
        return skillNames.Count == skillNames.Distinct().Count();
    }
}
