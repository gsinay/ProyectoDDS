using Fire_Emblem.Controllers.Handlers;
using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Player;
using Fire_Emblem.Views.CombatLoggers;

namespace Fire_Emblem.Controllers.Combat;

public class Combat
{
    
    private readonly Player[] _players;
    private int _turn;
    private readonly SpanishLogger _combatLog;
    private readonly WtbHandler _wtbHandler = new();
    private readonly SkillsHandler _skillsHandler = new();
    private readonly PlayerHandler _playerHandler = new();
    private readonly AttackHandler _attackHandler = new();

    public Combat(SpanishLogger logger, Fire_Emblem.Setup setup)
    {
        _players = setup.Players;
        _turn = 0;
        _combatLog = logger;

    }
    
    public void StartCombat()
    {
        ApplyMatchBasedEffects();
        
        while (_playerHandler.IsAlive(_players[0]) && _playerHandler.IsAlive(_players[1]))
        {
            ExecuteTurn(_players[_turn % 2], _players[(_turn + 1) % 2]);
            _turn++;
        }
        PrintEndGameMessage();
    }

    private void ApplyMatchBasedEffects()
    {
        foreach (var player in _players)
        {
            foreach (var character in player.Characters)
            {
                _skillsHandler.ApplyPermanentEffects(character); 
            }
        }
    }
    private void ExecuteTurn(Player attacker, Player defender)
    {
        Character attackChar = SelectAndPrepareCharacter(attacker);
        Character defendingChar = SelectAndPrepareCharacter(defender);

        ApplySkills(attackChar, defendingChar);

        PrintPreAttackLogs(attacker, attackChar, defendingChar);
    
        PerformAttacks(attackChar, defendingChar);
    
        _combatLog.AnnounceResults(attackChar, defendingChar);

        PostAttackCharacterUpdate(attackChar, defendingChar);
       
    
        CheckAndRemoveDeadCharacter(attacker, attackChar);
        CheckAndRemoveDeadCharacter(defender, defendingChar);
    }

    private Character SelectAndPrepareCharacter(Player player)
    {
        Character character = SelectCharacter(player);
        character.IsInitiatingCombat = (_turn % 2 + 1 == player.PlayerNumber);
        character.MarkAsNotAttacked();
        return character;

    }

    private Character SelectCharacter(Player player)
    {
        _combatLog.AnnounceOption(player.PlayerNumber);
        _combatLog.ListCharacters(player);
        string selectionInput = _combatLog.GetUserInput();
        int selection = int.Parse(selectionInput); 
        return player.Characters[selection];
    }

    private void ApplySkills(Character attacker, Character defender)
    {
        
        _skillsHandler.ApplySkillsBeforeCombat(attacker, defender, 1);
        _skillsHandler.ApplySkillsBeforeCombat(defender, attacker, 1);

        _skillsHandler.ApplySkillsBeforeCombat(attacker, defender, 2);
        _skillsHandler.ApplySkillsBeforeCombat(defender, attacker, 2);
        
        _skillsHandler.ApplySkillsBeforeCombat(defender, attacker,3 );
        _skillsHandler.ApplySkillsBeforeCombat(attacker, defender, 3);

    }

    private void PrintPreAttackLogs(Player attacker, Character attackChar, Character defendingChar)
    {
        _combatLog.AnnounceTurn(_turn, attackChar.Info.Name, attacker.PlayerNumber);
        double wtb = _wtbHandler.GetTriangleAdvantage(attackChar, defendingChar);
        _combatLog.PrintAdvantage(attackChar, defendingChar, wtb);
        _combatLog.PrintLog(attackChar, defendingChar);
    }
    private void PerformAttacks(Character attacker, Character defender)
    {
        attacker.MarkHasInitiatedCombat();
        defender.MarkHasDefendedCombat();
        Attack(attacker, defender);
        if (!defender.IsAlive()) return;
        Attack(defender, attacker);
        attacker.MarkAsAttacked();
        defender.MarkAsAttacked();
        attacker.ResetFirstAttackModifiers();
        defender.ResetFirstAttackModifiers();
        if (!attacker.IsAlive()) return;
        HandleFollowUps(attacker, defender);
    }
    private void Attack(Character attacker, Character defender)
    {
        int rawDamage = _attackHandler.CalculateRawInflictedDamage(attacker, defender);

        int realDamage = _attackHandler.CalculateReducedDamage(rawDamage, defender);
        defender.TakeDamage(realDamage);
        _combatLog.DisplayAttackResult(attacker, defender, realDamage);
    }

    private void HandleFollowUps(Character attacker, Character defender)
    {
        bool noFollowUps = true;
        if (CanFollowUp(attacker, defender))
        {
            Attack(attacker, defender);
            noFollowUps = false;
        }
        if (CanFollowUp(defender, attacker))
        {
            Attack(defender, attacker);
            noFollowUps = false;
        }
        if (noFollowUps)
            _combatLog.AnnounceNoFollowUps();
    }

    private bool CanFollowUp(Character attacker, Character defender)
    {
        return attacker.EffectiveSpd - defender.EffectiveSpd >= 5;
    }

    private void PostAttackCharacterUpdate(Character attacker, Character defender)
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
        Player winner = _playerHandler.IsAlive(_players[0]) ? _players[0] : _players[1];
        _combatLog.AnnounceWinner(winner.PlayerNumber);
    }
    
}