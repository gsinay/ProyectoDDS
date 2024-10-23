using Fire_Emblem.Models.Player;

namespace Fire_Emblem.Controllers.Handlers;

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