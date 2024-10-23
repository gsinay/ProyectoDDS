using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Collections;

namespace Fire_Emblem.Models.Player;

public class Player
{
    private readonly CharacterList _characters;
    public int PlayerNumber { get; }

    public Player(int playerNumber)
    {
       
        _characters = new CharacterList();
        PlayerNumber = playerNumber;
    }
    

    public void AddCharacter(Character character)
    {
        _characters.AddCharacter(character);
    }

    public int CharacterCount()
    {
        return _characters.Count();
    }
    public List<Character> Characters => _characters.GetCharacters();
    
    public string GetCharacterName(int characterIndex)
    {
        Character selectedCharacter = Characters[characterIndex];
        return selectedCharacter.Info.Name;
    }

    public void RemoveCharacter(Character character)
    {
        if (_characters.Contains(character))
            _characters.Remove(character);
    }
    

}