using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Player;
using Fire_Emblem.Views.CombatLoggers;

namespace Fire_Emblem.Controllers.Setup;

public class GUISetup : BaseSetup
{
    private CharacterBuilder _characterBuilder;
    private ILogger _menuView;
    
    public GUISetup(ILogger menuView)
    {
        _menuView = menuView;
        _characterBuilder = new CharacterBuilder();
    }
    
    public override void SetUpGame()
    {
        SetUpPlayers();
    }
    
    public void SetUpPlayers()
    {
        string[] lines = _menuView.GetTeamsFromUser();
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