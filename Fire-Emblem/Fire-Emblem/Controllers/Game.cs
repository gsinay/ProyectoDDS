using Fire_Emblem_View;
using Fire_Emblem.Exceptions;
using Fire_Emblem.SkillsManager;
using Fire_Emblem.Views;
using Fire_Emblem.Views.MainMenu;


namespace Fire_Emblem;

public class Game
{
    private readonly View _view;
    private readonly Setup _setup;
    private readonly SpanishLogger _logger;
    private readonly SpanishMenu _menuView;

    

    public Game(View view, string teamsFolder)
    {
        _view = view;
        _menuView = new SpanishMenu(_view);
        _setup = new Setup(_menuView, teamsFolder, new JsonHandler(), new SkillFactory());
        _logger = new SpanishLogger(_view);

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
                _menuView.PrintFileNotValid();
            }
        }
        catch (SkillNotImplementedException ex) 
        {
            _menuView.PrintFileNotValid();
        }
       
    }
}