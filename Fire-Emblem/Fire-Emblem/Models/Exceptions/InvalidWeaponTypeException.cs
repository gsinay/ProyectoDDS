namespace Fire_Emblem.Models.Exceptions;

public class InvalidWeaponTypeException : Exception
{
    public InvalidWeaponTypeException(string name)
        : base($"Invalid weapon type: {name}")
    {
    }
}