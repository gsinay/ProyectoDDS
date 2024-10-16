using Fire_Emblem_View;

namespace Fire_Emblem.Views.MainMenu;

public abstract class AbstractMenu
{
    protected readonly View _view;

    public AbstractMenu(View view)
    {
        _view = view;
    }
    
    public string  GetUserInput()
    {
        return _view.ReadLine();
    }

    public abstract void AskForFile();

    public abstract void PrintNoFileFound();

    public void PrintFileNumberAndName(int number, string fileName)
    {
        _view.WriteLine($"{number}: {fileName}");
    }
    
    public abstract void PrintFileNotValid();
    
    
    
}