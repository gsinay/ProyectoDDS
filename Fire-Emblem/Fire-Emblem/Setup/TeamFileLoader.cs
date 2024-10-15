namespace Fire_Emblem;

public class TeamFileLoader
{
    private string _teamsFolder;

    public TeamFileLoader(string teamsFolder)
    {
        _teamsFolder = teamsFolder;
    }

    public string[] GetOrderedFiles()
    {
        if (!Directory.Exists(_teamsFolder)) return Array.Empty<string>();
        string[] files = Directory.GetFiles(_teamsFolder);
        Array.Sort(files);
        return files;
    }

    public string[] LoadTeamFromFile(string fileName)
    {
        return File.ReadAllLines(Path.Combine(_teamsFolder, fileName));
    }
}
