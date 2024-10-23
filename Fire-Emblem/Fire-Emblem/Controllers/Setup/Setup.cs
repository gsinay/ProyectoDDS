using Fire_Emblem.Controllers;
using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Player;
using Fire_Emblem.SkillsManager;
using Fire_Emblem.Views.MainMenu;

namespace Fire_Emblem;

public class Setup
{
    private SpanishMenu _menuView;
    private TeamFileLoader _fileLoader;
    private CharacterBuilder _characterBuilder;
    private Player[] _players;

    public Player[] Players => _players;

    public Setup(SpanishMenu menuView, string teamsFolder)
    {
        _menuView = menuView;
        _fileLoader = new TeamFileLoader(teamsFolder);
        _characterBuilder = new CharacterBuilder();
        _players = new Player[2];
        _players[0] = new Player(playerNumber: 1);
        _players[1] = new Player(playerNumber: 2);
    }

    public void SetUpGame()
    {
        _menuView.AskForFile();
        ListFiles();
        string selectedFile = _menuView.GetUserInput();
        string selectedFileName = FormatFileName(Convert.ToInt32(selectedFile));
        SetUpPlayers(selectedFileName);
       
    }

    private void ListFiles()
    {
        string[] files = _fileLoader.GetOrderedFiles();

        if (files.Length == 0)
        {
            _menuView.PrintNoFileFound();
            return;
        }

        DisplayFiles(files);
    }

    private void DisplayFiles(string[] files)
    {
        for (var i = 0; i < files.Length; i++)
        {
            var fileName = Path.GetFileName(files[i]);
            _menuView.PrintFileNumberAndName(i, fileName);
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
        var skillNames = character.Skills.Select(s => s.Name).ToList();
        return skillNames.Count == skillNames.Distinct().Count();
    }
}
