using Fire_Emblem_View;
using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class Combat
{
    public Player[] Players;
    private int _turn;

    private View _view;

    private CombatLog _combatlog;

    public Combat(View view, Setup setup)
    {
        Players = setup.Players;
        _turn = 0;
        _view = view;
        _combatlog = new CombatLog(_view);
        
    }
    
    public void StartCombat()
    {
        foreach (var player in Players)
        {
            foreach (var character in player.Characters)
            {
                character.ApplyPermanentEffects(); 
            }
        }
        while (Players[0].IsAlive() && Players[1].IsAlive())
        {
            ExecuteTurn(Players[_turn % 2], Players[(_turn + 1) % 2]);
            _turn++;
        }
        PrintEndGameMessage();
    }
    private void ExecuteTurn(Player attacker, Player defender)
    {
        Character attackChar = SelectAndPrepareCharacter(attacker);
        Character defendingChar = SelectAndPrepareCharacter(defender);
        
        attackChar.ApplySkillsBeforeCombat(defendingChar, _combatlog);
        defendingChar.ApplySkillsBeforeCombat(attackChar, _combatlog);

        PrintPreAttackLogs(attacker, attackChar, defendingChar);
    
        PerformAttacks(attackChar, defendingChar);
    
        _combatlog.AnnounceResults(attackChar, defendingChar);

        PostAttackCharacterUpdate(attackChar, defendingChar);
       
    
        CheckAndRemoveDeadCharacter(attacker, attackChar);
        CheckAndRemoveDeadCharacter(defender, defendingChar);
    }

    private Character SelectAndPrepareCharacter(Player player)
    {
        Character character = SelectCharacter(player);
        character.IsInitiatingCombat = (_turn % 2 + 1 == player.PlayerNumber);
        character.ResetHasAttackStatus();
        return character;

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

    private void PrintPreAttackLogs(Player attacker, Character attackChar, Character defendingChar)
    {
        _combatlog.AnnounceTurn(_turn, attackChar.Info.Name, attacker.PlayerNumber);
        double WTB = CheckTriangleAdvantage(attackChar, defendingChar);
        _combatlog.PrintAdvantage(attackChar, defendingChar, WTB);
        _combatlog.PrintLog(attackChar, defendingChar);
    }
    
    private double CheckTriangleAdvantage(Character attacker, Character defender)
    {
        var advantage = new Dictionary<string, string>
        {
            { "Sword", "Axe" }, { "Axe", "Lance" }, { "Lance", "Sword" }
        };
        if (advantage.ContainsKey(attacker.Info.Weapon) && advantage[attacker.Info.Weapon] == defender.Info.Weapon)
        {
            return 1.2; 
        }
        else if (advantage.ContainsKey(defender.Info.Weapon) && advantage[defender.Info.Weapon] == attacker.Info.Weapon)
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
        attacker.SetHasAttackedStatus();
        defender.SetHasAttackedStatus();
        attacker.ResetFirstAttackModifiers();
        defender.ResetFirstAttackModifiers();
        if (!attacker.IsAlive()) return;
        if (attacker.EffectiveSpd - defender.EffectiveSpd >= 5) Attack(attacker, defender);
        else if (defender.EffectiveSpd - attacker.EffectiveSpd >= 5) Attack(defender, attacker);
        else
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
    }
    private void Attack(Character attackChar, Character defendingChar)
    {
        double WTB = CheckTriangleAdvantage(attackChar, defendingChar);
        int effectiveDefense = attackChar.Info.Weapon == "Magic" ? defendingChar.EffectiveRes : defendingChar.EffectiveDef;
        int damage = Math.Max(0, (int)(attackChar.EffectiveAtk * WTB) - effectiveDefense);
        defendingChar.TakeDamage(damage); 
        _view.WriteLine($"{attackChar.Info.Name} ataca a {defendingChar.Info.Name} con {damage} de daño");

    }

    public void PostAttackCharacterUpdate(Character attacker, Character defender)
    {
        attacker.UpdateMostRecentOpponent(defender);
        defender.UpdateMostRecentOpponent(attacker);
        
        attacker.ResetModifiers();
        defender.ResetModifiers();
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