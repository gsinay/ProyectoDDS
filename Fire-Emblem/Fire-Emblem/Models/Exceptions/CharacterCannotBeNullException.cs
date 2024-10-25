namespace Fire_Emblem.Models.Exceptions;

public class CharacterCannotBeNullException : Exception
{
    public CharacterCannotBeNullException(string name)
        : base($"Character '{name}' does not exist")
    {
       
    }
}