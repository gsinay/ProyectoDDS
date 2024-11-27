using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Collections;
using Fire_Emblem.Models.Names;

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
    public IReadOnlyList<Character> Characters => _characters.GetCharacters();
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
    
    public MyUnit GetMyUnit(int characterIndex)
    {
        Character character = Characters[characterIndex];
        return new MyUnit(character.Info.Name, 
            character.Info.Weapon.ToString(), 
            character.GetHp, 
            character.Stats.BaseStats.GetBaseStat(StatName.Atk), 
            character.Stats.BaseStats.GetBaseStat(StatName.Spd), 
            character.Stats.BaseStats.GetBaseStat(StatName.Def), 
            character.Stats.BaseStats.GetBaseStat(StatName.Res));
    }
    public MyUnit[] GetMyIUnits()
    {
        MyUnit[] myUnits = new MyUnit[CharacterCount()];
        for (int i = 0; i < CharacterCount(); i++)
        {
            Character character = Characters[i];
            myUnits[i] = new MyUnit(character.Info.Name, 
                character.Info.Weapon.ToString(), 
                character.GetHp, 
                character.Stats.BaseStats.GetBaseStat(StatName.Atk), 
                character.Stats.BaseStats.GetBaseStat(StatName.Spd), 
                character.Stats.BaseStats.GetBaseStat(StatName.Def), 
                character.Stats.BaseStats.GetBaseStat(StatName.Res));
        }
        return myUnits;
    }
    

}