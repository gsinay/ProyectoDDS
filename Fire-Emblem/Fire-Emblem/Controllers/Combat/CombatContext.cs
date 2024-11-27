using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Player;

namespace Fire_Emblem.Controllers.Combat;

public class CombatContext
{
    public Player Attacker { get; }
    public Player Defender { get; }
    public Character AttackChar { get; }
    public Character DefendingChar { get; }
    public int DamageDealt { get; }

    public CombatContext(Player attacker, Player defender, Character attackChar, Character defendingChar, int damageDealt)
    {
        Attacker = attacker;
        Defender = defender;
        AttackChar = attackChar;
        DefendingChar = defendingChar;
        DamageDealt = damageDealt;
    }
}