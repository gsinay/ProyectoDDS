namespace Fire_Emblem.Models.Exceptions;

public class SkillNotImplementedException : Exception
{
    public SkillNotImplementedException()
        : base("Archivo de equipos no válido")
    {
    }
}