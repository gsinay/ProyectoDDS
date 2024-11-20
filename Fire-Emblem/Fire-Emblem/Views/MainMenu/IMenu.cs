using Fire_Emblem_View;

namespace Fire_Emblem.Views.MainMenu
{
    public interface IMenu
    {
        string GetUserInput();
        void AskForFile();
        void PrintNoFileFound();
        void PrintFileNumberAndName(int number, string fileName);
        void PrintFileNotValid();
    }
}