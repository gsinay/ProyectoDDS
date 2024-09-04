using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class Player
{
    private CharacterList characters;
    public int PlayerNumber;

    public Player(int playerNumber)
    {
       
        characters = new CharacterList();
        PlayerNumber = playerNumber;
    }

    public void AddCharacter(Character character)
    {
        characters.AddCharacter(character);
    }

    public int CharacterCount()
    {
        return characters.Count();
    }
    public List<Character> Characters => characters.GetCharacters();
    
    public string GetCharacterName(int characterIndex)
    {
        Character selectedCharacter = Characters[characterIndex];
        return selectedCharacter.Name;
    }
    public bool IsAlive()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            if (Characters[i].GetHP > 0)
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