using Fire_Emblem.Controllers;
using Fire_Emblem.Controllers.Setup;
using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Player;
using Fire_Emblem.Views.CombatLoggers;

namespace Fire_Emblem;

public class CLISetup : BaseSetup
{
    private ILogger _menuView;
    private TeamFileLoader _fileLoader;
    private CharacterBuilder _characterBuilder;


    public CLISetup(ILogger menuView, string teamsFolder)
    {
        _menuView = menuView;
        _fileLoader = new TeamFileLoader(teamsFolder);
        _characterBuilder = new CharacterBuilder();
    }

    public override void SetUpGame()
    {
        _menuView.AskForFile();
        ListFiles();
        int selectedFile = _menuView.GetUserFileInput();
        string selectedFileName = FormatFileName(selectedFile);
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
                Player player = _players[currentTeam];
                Character character = _characterBuilder.CreateCharacter(line);
                character.AssignOwner(player);
                player.AddCharacter(character);
            }
        }
    }
}