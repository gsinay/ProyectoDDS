using Fire_Emblem.Models.Player;
using Fire_Emblem.Models.Characters;

namespace Fire_Emblem;

public abstract class BaseSetup
{
    protected readonly Player[] _players;

    public Player[] Players => _players;

    protected BaseSetup()
    {
        _players = new Player[2];
        _players[0] = new Player(1);
        _players[1] = new Player(2);
    }
    
    public abstract void SetUpGame();

    public bool AreTeamsValid()
    {
        return IsTeamValid(_players[0]) && IsTeamValid(_players[1]);
    }

    public bool IsTeamValid(Player player)
    {
        return IsCharacterCountValid(player) && AreCharacterNamesUnique(player) && IsSkillCountValid(player);
    }

    private bool IsCharacterCountValid(Player player)
    {
        return player.CharacterCount() is >= 1 and <= 3;
    }

    private bool AreCharacterNamesUnique(Player player)
    {
        var characterNames = player.Characters.Select(c => c.Info.Name).ToList();
        return characterNames.Count == characterNames.Distinct().Count();
    }

    private bool IsSkillCountValid(Player player)
    {
        foreach (var character in player.Characters)
        {
            if (character.SkillCount() > 2 || !AreSkillNamesUnique(character))
                return false;
        }
        return true;
    }

    private bool AreSkillNamesUnique(Character character)
    {
        var skillNames = character.Skills.Select(s => s.Name).ToList();
        return skillNames.Count == skillNames.Distinct().Count();
    }
    
}