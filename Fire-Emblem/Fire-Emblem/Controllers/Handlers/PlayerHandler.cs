using Fire_Emblem.Characters;

namespace Fire_Emblem.Handlers;

public class PlayerHandler
{
    
    public bool IsAlive(Player player)
    {
        foreach (var character in player.Characters)
        {
            if (character.GetHp > 0)
                return true;
        }

        return false;
    }
    
}