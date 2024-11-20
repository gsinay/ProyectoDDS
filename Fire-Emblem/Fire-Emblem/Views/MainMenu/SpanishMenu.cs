using Fire_Emblem_View;

namespace Fire_Emblem.Views.MainMenu
{
    public class SpanishMenu : IMenu
    {
        private readonly View _view;

        public SpanishMenu(View view)
        {
            _view = view;
        }

        public string GetUserInput()
        {
            return _view.ReadLine();
        }

        public void AskForFile()
        {
            _view.WriteLine("Elige un archivo para cargar los equipos");
        }

        public void PrintNoFileFound()
        {
            _view.WriteLine("No se encontraron archivos de equipos.");
        }

        public void PrintFileNumberAndName(int number, string fileName)
        {
            _view.WriteLine($"{number}: {fileName}");
        }

        public void PrintFileNotValid()
        {
            _view.WriteLine("Archivo de equipos no válido");
        }
    }
}