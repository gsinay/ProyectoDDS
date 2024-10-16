namespace Fire_Emblem.Exceptions;

public class CharacterDoesntExistException : Exception
{
    public CharacterDoesntExistException(string name)
        : base($"Character '{name}' does not exist")
    {
    }
}