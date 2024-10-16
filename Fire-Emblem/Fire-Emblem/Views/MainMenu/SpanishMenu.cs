using Fire_Emblem_View;

namespace Fire_Emblem.Views.MainMenu;

public class SpanishMenu : AbstractMenu
{
    public SpanishMenu(View view) : base(view){}

    public override void AskForFile()
    {
        _view.WriteLine($"Elige un archivo para cargar los equipos");
    }

    public override void PrintNoFileFound()
    {
        _view.WriteLine("No se encontraron archivos de equipos.");
    }

    public override void PrintFileNotValid()
    {
        _view.WriteLine("Archivo de equipos no válido");
    }

   
}