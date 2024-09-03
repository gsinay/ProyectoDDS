using Fire_Emblem_View;

namespace Fire_Emblem;

public class Game
{
    private View _view;
    private string _teamsFolder;
    private Setup _setup;
  
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
        _setup = new Setup(_view, _teamsFolder);
    }

    public void Play()
    {
        _setup.SetUpGame();
        if (_setup.IsValidTeams())
        {
            Combat combat = new Combat(_view, _setup);
            combat.StartCombat();
        }
        else
            _view.WriteLine("Archivo de equipos no válido");

    }
}