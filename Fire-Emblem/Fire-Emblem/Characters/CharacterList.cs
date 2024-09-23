namespace Fire_Emblem.Characters;

public class CharacterList
{
    private List<Character?> _characters;

    public CharacterList()
    {
        _characters = new List<Character?>();
    }

    public void AddCharacter(Character? character)
    {
        _characters.Add(character);
    }

    public int Count()
    {
        return _characters.Count;
    }

    public List<Character> GetCharacters() => _characters;
    
}