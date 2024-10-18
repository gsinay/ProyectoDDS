using Fire_Emblem.Characters;
using Fire_Emblem.Handlers;
using Fire_Emblem.Views;


namespace Fire_Emblem.Combat;

public class Combat
{
    
    private readonly Player[] _players;
    private int _turn;
    private readonly SpanishLogger _combatlog;
    private readonly WtbHandler _wtbHandler = new();

    public Combat(SpanishLogger logger, Setup setup)
    {
        _players = setup.Players;
        _turn = 0;
        _combatlog = logger;

    }
    
    public void StartCombat()
    {
        ApplyMatchBasedEffects();
        
        while (_players[0].IsAlive() && _players[1].IsAlive())
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
                character.ApplyPermanentEffects(); 
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
    
        _combatlog.AnnounceResults(attackChar, defendingChar);

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
        _combatlog.AnnounceOption(player.PlayerNumber);
        _combatlog.ListCharacters(player);
        string selectionInput = _combatlog.GetUserInput();
        int selection = int.Parse(selectionInput); 
        return player.Characters[selection];
    }

    private void ApplySkills(Character attacker, Character defender)
    {
        attacker.ApplyBasicSkillsBeforeCombat(defender);
        defender.ApplyBasicSkillsBeforeCombat(attacker);

        attacker.ApplySecondDegreeSkillsBeforeCombat(defender);
        defender.ApplySecondDegreeSkillsBeforeCombat(attacker);
        
        defender.ApplyThirdDegreeSkillsBeforeCombat(attacker);
        attacker.ApplyThirdDegreeSkillsBeforeCombat(defender);
        


    }

    private void PrintPreAttackLogs(Player attacker, Character attackChar, Character defendingChar)
    {
        _combatlog.AnnounceTurn(_turn, attackChar.Info.Name, attacker.PlayerNumber);
        double wtb = _wtbHandler.GetTriangleAdvantage(attackChar, defendingChar);
        _combatlog.PrintAdvantage(attackChar, defendingChar, wtb);
        _combatlog.PrintLog(attackChar, defendingChar);
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
        double wtb = _wtbHandler.GetTriangleAdvantage(attacker, defender);

        int attackPower = (int)(attacker.EffectiveAtk * wtb);
        
        
        int effectiveDefense = GetEffectiveDefense(attacker, defender);
        
        
        var atkModifierExtra = attacker.GetAttackModifier();
        
        int rawDamage = Math.Max(0, attackPower - effectiveDefense) + atkModifierExtra;
        
        int damage = CalculateReducedDamage(rawDamage, defender);
        

        defender.TakeDamage(damage);
        _combatlog.DisplayAttackResult(attacker, defender, damage);
    }
    private int GetEffectiveDefense(Character attacker, Character defender)
    {
        if (attacker.Info.Weapon == WeaponName.Magic)
        {
            return defender.EffectiveRes;
        }
       
        return defender.EffectiveDef;
    }

    private int CalculateReducedDamage(int rawDamage, Character defender) => defender.GetAttackWithReduction(rawDamage);

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
            _combatlog.AnnounceNoFollowUps();
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
        Player winner = _players[0].IsAlive() ? _players[0] : _players[1];
        _combatlog.AnnounceWinner(winner.PlayerNumber);
    }
    
}