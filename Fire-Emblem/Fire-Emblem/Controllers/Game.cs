using Fire_Emblem_View;
using Fire_Emblem.Controllers.Setup;
using Fire_Emblem.Models.Exceptions;
using Fire_Emblem.Views.CombatLoggers;
namespace Fire_Emblem.Controllers;

public class Game
{
    private readonly BaseSetup _setup;
    private readonly ILogger _logger;
    
    public Game(View view, string teamsFolder)
    {
        _logger = new SpanishLogger(view);
        _setup = new CLISetup(_logger, teamsFolder);
    }
    
    public Game(WindowView windowLogger) {
        _logger = windowLogger; 
        _setup = new GUISetup(_logger);
    }
    public void Play()
    {
        try
        {
            _setup.SetUpGame(); 
            if (_setup.AreTeamsValid())
            {
                Combat.Combat combat = new Combat.Combat(_logger, _setup);
                combat.StartCombat();
            }
            else
            {
                _logger.PrintGameNotValid();
            }
        }
        catch (SkillNotImplementedException) 
        {
            _logger.PrintGameNotValid();
        }
       
    }
}