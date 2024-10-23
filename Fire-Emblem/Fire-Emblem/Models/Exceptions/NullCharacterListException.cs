namespace Fire_Emblem.Models.Exceptions;

public class NullCharacterListException() : Exception($"Error during deserialization: character list is null");