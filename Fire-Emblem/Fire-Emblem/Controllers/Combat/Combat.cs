using Fire_Emblem.Controllers.Handlers;
using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Player;
using Fire_Emblem.Views.CombatLoggers;

namespace Fire_Emblem.Controllers.Combat;

public class Combat
{
    
    private readonly Player[] _players;
    private int _turn;
    private readonly ILogger _combatLog;
    private readonly WtbHandler _wtbHandler = new();
    private readonly SkillsHandler _skillsHandler = new();
    private readonly PlayerHandler _playerHandler = new();
    private readonly AttackHandler _attackHandler = new();

    public Combat(ILogger logger, Fire_Emblem.BaseSetup setup)
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
        _combatLog.UpdateTeams(attacker, defender);
        
        Character attackChar = SelectAndPrepareCharacter(attacker);
        Character defendingChar = SelectAndPrepareCharacter(defender);

        ApplySkills(attackChar, defendingChar);
        
        LoadPreAttackHpReduction(attackChar, defendingChar);

        PrintPreAttackLogs(attacker, attackChar, defendingChar);
    
        PerformAttacks(attackChar, defendingChar);
        
        if (attackChar.IsAlive() && defendingChar.IsAlive()) HandleFollowUps(attackChar, defendingChar);
        
        ApplyAfterCombatEffects(attackChar, defendingChar);
        
        PrintAfterCombatLogs(attackChar, defendingChar);
        
        PostAttackCharacterUpdate(attackChar, defendingChar);
        
        CheckAndRemoveDeadCharacter(attacker, attackChar);
        CheckAndRemoveDeadCharacter(defender, defendingChar);
    }

    private Character SelectAndPrepareCharacter(Player player)
    {
        Character character = SelectCharacter(player);
        character.MarkAsNotAttacked();
        character.IsInitiatingCombat = (_turn % 2 + 1 == player.PlayerNumber);
        return character;

    }

    private Character SelectCharacter(Player player)
    {
        _combatLog.AnnounceOption(player.PlayerNumber);
        _combatLog.ListCharacters(player);
        int selection = _combatLog.GetUserCharacterInput(player);
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
    
    private void LoadPreAttackHpReduction(Character attacker, Character defender)
    {
        int attackerHpReduction = attacker.CharacterModifiers.CombatModifiers.BeforeCombatHpReduction;
        int defenderHpReduction = defender.CharacterModifiers.CombatModifiers.BeforeCombatHpReduction;
        attacker.TakeOutsideAttackDamage(attackerHpReduction);
        defender.TakeOutsideAttackDamage(defenderHpReduction);
    }

    private void PrintPreAttackLogs(Player attacker, Character attackChar, Character defendingChar)
    {
        _combatLog.AnnounceTurn(_turn, attackChar.Info.Name, attacker.PlayerNumber);
        double wtb = _wtbHandler.GetTriangleAdvantage(attackChar, defendingChar);
        _combatLog.PrintAdvantage(attackChar, defendingChar, wtb);
        _combatLog.PrintPreCombatLog(attackChar, defendingChar);
    }
    private void PerformAttacks(Character attacker, Character defender)
    {
        attacker.MarkHasInitiatedCombat();
        defender.MarkHasDefendedCombat();
        Attack(attacker, defender, "first");
        if (!_attackHandler.CanCounterAttack(attacker, defender)) return;
        Attack(defender, attacker, "first");
        attacker.ResetFirstAttackModifiers();
        defender.ResetFirstAttackModifiers();
       
    }

   

    private void Attack(Character attacker, Character defender, string attackType)
    {
        int damageDealt = CalculateDamage(attacker, defender, attackType); 
        _combatLog.DisplayAttackResult(attacker, defender, damageDealt); 
        attacker.MarkAsAttacked();
        defender.TakeDamage(damageDealt);

        HandleAttackHealingAndLog(attacker, damageDealt); 
    }
    
    private int CalculateDamage(Character attacker, Character defender, string attackType)
    {
        int rawDamage = _attackHandler.CalculateRawInflictedDamage(attacker, defender, attackType);
        int realDamage = _attackHandler.CalculateReducedDamage(rawDamage, defender, attackType);
        return realDamage;
    }
    
    private void HandleAttackHealingAndLog(Character character, int damageDealt)
    {
        int healing = character.PercentHealAfterAttack(damageDealt);
        _combatLog.DisplayHealingResult(character, healing);
    }
    

    private void HandleFollowUps(Character attacker, Character defender)
    {
        bool noFollowUps = true;
        if (CanFollowUp(attacker, defender))
        {
            Attack(attacker, defender, "followup");
            noFollowUps = false;
        }
        if (CanFollowUp(defender, attacker))
        {
            Attack(defender, attacker, "followup");
            noFollowUps = false;
        }
        if (noFollowUps)
            _combatLog.AnnounceNoFollowUps(attacker, defender);
    }

    private bool CanFollowUp(Character attacker, Character defender)
    {
        if(!attacker.IsAlive() || !defender.IsAlive()) return false;
        bool hasNegatedCounterAttack = attacker.CharacterModifiers.CombatModifiers.CounterAttackIsNegated && 
                                       !attacker.CharacterModifiers.CombatModifiers.NegatedCounterAttackNegation;
        if (hasNegatedCounterAttack) return false;
        
        int FollowUpCount = CountFollowUpPossibilities(attacker, defender);
        return FollowUpCount > 0;
    }

    private int CountFollowUpPossibilities(Character attacker, Character defender)
    {
        int count = 0;
        if (attacker.EffectiveSpd("followup") - defender.EffectiveSpd("followup") >= 5)
            count++;
        if(!attacker.CharacterModifiers.CombatModifiers.NegatedGuaranteedFollowup)
            count += attacker.CharacterModifiers.CombatModifiers.GuaranteedFollowupCounter;
        if(!attacker.CharacterModifiers.CombatModifiers.NegateNegatedFollowup)
            count -= attacker.CharacterModifiers.CombatModifiers.NegatedFollowupCounter;
        return count;
    }

    private void ApplyAfterCombatEffects(Character attacker, Character defender)
    {
        LoadAfterCombatHpChange(attacker);
        LoadAfterCombatHpChange(defender);
    }

    private void LoadAfterCombatHpChange(Character character)
    {
        if (character.IsAlive())
            character.ChangeHp();
        
    }
    
    
    private void PrintAfterCombatLogs(Character attacker, Character defender)
    {
        _combatLog.PrintPostCombatLog(attacker, defender);
        _combatLog.AnnounceResults(attacker, defender);

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