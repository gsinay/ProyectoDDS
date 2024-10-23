namespace Fire_Emblem.Models.Exceptions;

public class CharacterDoesntExistException : Exception
{
    public CharacterDoesntExistException(string name)
        : base($"Character '{name}' does not exist")
    {
    }
}