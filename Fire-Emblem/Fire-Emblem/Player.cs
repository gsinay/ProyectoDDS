using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class Player
{
    private CharacterList _characters;
    public int PlayerNumber;

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
    public bool IsAlive()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            if (Characters[i].GetHp > 0)
                return true;
        }
        
        return false;
    }

    public void RemoveCharacter(Character character)
    {
        if (Characters.Contains(character))
            Characters.Remove(character);
    }

}