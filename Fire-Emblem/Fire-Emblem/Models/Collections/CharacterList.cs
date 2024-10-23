using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Collections;

public class CharacterList
{
    private readonly List<Character> _characters;

    public CharacterList()
    {
        _characters = new List<Character>();
    }

    public void AddCharacter(Character character)
    {
        _characters.Add(character);
    }

    public int Count()
    {
        return _characters.Count;
    }

    public List<Character> GetCharacters() => _characters;

    public bool Contains(Character character) => _characters.Contains(character);

    public void Remove(Character character) => _characters.Remove(character);

}