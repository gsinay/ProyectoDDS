using Fire_Emblem_View;
using Fire_Emblem.Combat;
using Fire_Emblem.Exceptions;
using Fire_Emblem.SkillsManager;



namespace Fire_Emblem;

public class Game
{
    private readonly View _view;
    private readonly Setup _setup;
    

    public Game(View view, string teamsFolder)
    {
        _view = view;
        _setup = new Setup(_view, teamsFolder, new JsonHandler(), new SkillFactory());
        
    }
    public void Play()
    {
        try
        {
            _setup.SetUpGame(); 
            if (_setup.AreTeamsValid()) 
            {
                Combat.Combat combat = new Combat.Combat(_view, _setup);
                combat.StartCombat();
            }
            else
            {
                _view.WriteLine("Archivo de equipos no válido");
            }
        }
        catch (SkillNotImplementedException ex) 
        {
            _view.WriteLine("Archivo de equipos no válido");
        }
       
    }
}