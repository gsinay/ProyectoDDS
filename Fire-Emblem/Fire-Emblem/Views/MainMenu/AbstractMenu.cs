using Fire_Emblem_View;

namespace Fire_Emblem.Views.MainMenu;

public abstract class AbstractMenu
{
    protected readonly View View;

    public AbstractMenu(View view)
    {
        View = view;
    }
    
    public string  GetUserInput()
    {
        return View.ReadLine();
    }

    public abstract void AskForFile();

    public abstract void PrintNoFileFound();

    public void PrintFileNumberAndName(int number, string fileName)
    {
        View.WriteLine($"{number}: {fileName}");
    }
    
    public abstract void PrintFileNotValid();
    
    
    
}