namespace Fire_Emblem.Exceptions;

public class InvalidWeaponTypeException : Exception
{
    public InvalidWeaponTypeException(string name)
        : base($"Invalid weapon type: {name}")
    {
    }
}