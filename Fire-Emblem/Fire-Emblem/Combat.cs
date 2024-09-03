using Fire_Emblem_View;

namespace Fire_Emblem;

public class Combat
{
    public Player[] Players;
    public int Turno { get; private set;  }

    private View _view;

    public Combat(View view, Setup setup)
    {
        Players = setup.Players;
        Turno = 0;
        _view = view;
    }
    
    public void StartCombat()
    {
        while (Players[0].IsAlive() && Players[1].IsAlive())
        {
            ExecuteTurn(Players[Turno % 2], Players[(Turno + 1) % 2]);
            Turno++;
        }
        PrintEndGameMessage();
    }
    private void ExecuteTurn(Player attacker, Player defender)
    {
        Character attackChar = SelectCharacter(attacker);
        Character defendingChar = SelectCharacter(defender);
        _view.WriteLine($"Round {Turno + 1}: {attackChar.Name} (Player {attacker.PlayerNumber}) comienza");
        PrintAdvantage(attackChar, defendingChar);
        PerformAttacks(attackChar, defendingChar);
        _view.WriteLine($"{attackChar.Name} ({attackChar.GetHP}) : {defendingChar.Name} ({defendingChar.GetHP})");
        CheckAndRemoveDeadCharacter(attacker, attackChar);
        CheckAndRemoveDeadCharacter(defender, defendingChar);
    }
    private Character SelectCharacter(Player player)
    {
        _view.WriteLine($"Player {player.PlayerNumber} selecciona una opción");
        ListCharacters(player);
        int selection = Convert.ToInt32(_view.ReadLine());
        return player.Characters[selection];

    }
    private void ListCharacters(Player player)
    {
        for (int i = 0; i < player.CharacterCount(); i++)
        {
            _view.WriteLine($"{i}: {player.GetCharacterName(i)}");
        }
    }
    

    private void PrintAdvantage(Character attacker, Character defender)
    {
        double WTB = CheckTriangleAdvantage(attacker, defender);
        if (WTB == 1.2)
            _view.WriteLine($"{attacker.Name} ({attacker.Weapon}) tiene ventaja con respecto a {defender.Name} ({defender.Weapon})");
        else if(WTB == 0.8)
            _view.WriteLine($"{defender.Name} ({defender.Weapon}) tiene ventaja con respecto a {attacker.Name} ({attacker.Weapon})");
        else
        {
            _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
        }
    }
    
    private double CheckTriangleAdvantage(Character attacker, Character defender)
    {
        var advantage = new Dictionary<string, string>
        {
            { "Sword", "Axe" }, { "Axe", "Lance" }, { "Lance", "Sword" }
        };
        if (advantage.ContainsKey(attacker.Weapon) && advantage[attacker.Weapon] == defender.Weapon)
        {
            return 1.2; 
        }
        else if (advantage.ContainsKey(defender.Weapon) && advantage[defender.Weapon] == attacker.Weapon)
        {
            return 0.8;
        }
        return 1;
    }
    
    private void PerformAttacks(Character attacker, Character defender)
    {
        Attack(attacker, defender);
        if (!defender.IsAlive()) return;
        Attack(defender, attacker);
        if (!attacker.IsAlive()) return;
        if (attacker.Spd - defender.Spd >= 5) Attack(attacker, defender);
        else if (defender.Spd - attacker.Spd >= 5) Attack(defender, attacker);
        else
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
    }
    private void Attack(Character attackChar, Character defendingChar)
    {
        double WTB = CheckTriangleAdvantage(attackChar, defendingChar);
        int effectiveDefense = attackChar.Weapon == "Magic" ? defendingChar.Res : defendingChar.Def;
        int damage = Math.Max(0, (int)(attackChar.Atk * WTB) - effectiveDefense);
        defendingChar.TakeDamage(damage); 
        _view.WriteLine($"{attackChar.Name} ataca a {defendingChar.Name} con {damage} de daño");

    }

    private void CheckAndRemoveDeadCharacter(Player player, Character character)
    {
        if (!character.IsAlive())
            player.RemoveCharacter(character);
    }

    private void PrintEndGameMessage()
    {
        Player winner = Players[0].IsAlive() ? Players[0] : Players[1];
        _view.WriteLine($"Player {winner.PlayerNumber} ganó");
    }
    
}