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

        var combatContext = new CombatContext(attacker, defender, attackChar, defendingChar);

        ApplySkills(combatContext);
        LoadPreAttackHpReduction(combatContext);

        PrintPreAttackLogs(combatContext);

        PerformAttacks(combatContext);

        if (combatContext.AttackChar.IsAlive() && combatContext.DefendingChar.IsAlive())
            HandleFollowUps(combatContext);

        ApplyAfterCombatEffects(combatContext);
        PrintAfterCombatLogs(combatContext);

        PostAttackCharacterUpdate(combatContext);

        CheckAndRemoveDeadCharacter(combatContext.Attacker, combatContext.AttackChar);
        CheckAndRemoveDeadCharacter(combatContext.Defender, combatContext.DefendingChar);
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

    private void ApplySkills(CombatContext context)
    {
        _skillsHandler.ApplySkillsBeforeCombat(context.AttackChar, context.DefendingChar, 1);
        _skillsHandler.ApplySkillsBeforeCombat(context.DefendingChar, context.AttackChar, 1);

        _skillsHandler.ApplySkillsBeforeCombat(context.AttackChar, context.DefendingChar, 2);
        _skillsHandler.ApplySkillsBeforeCombat(context.DefendingChar, context.AttackChar, 2);

        _skillsHandler.ApplySkillsBeforeCombat(context.DefendingChar, context.AttackChar, 3);
        _skillsHandler.ApplySkillsBeforeCombat(context.AttackChar, context.DefendingChar, 3);
    }

    private void LoadPreAttackHpReduction(CombatContext context)
    {
        int attackerHpReduction = context.AttackChar.CharacterModifiers.CombatModifiers.BeforeCombatHpReduction;
        int defenderHpReduction = context.DefendingChar.CharacterModifiers.CombatModifiers.BeforeCombatHpReduction;
        context.AttackChar.TakeOutsideAttackDamage(attackerHpReduction);
        context.DefendingChar.TakeOutsideAttackDamage(defenderHpReduction);
    }

    private void PrintPreAttackLogs(CombatContext context)
    {
        _combatLog.AnnounceTurn(_turn, context.AttackChar.Info.Name, context.Attacker.PlayerNumber);
        double wtb = _wtbHandler.GetTriangleAdvantage(context.AttackChar, context.DefendingChar);
        _combatLog.PrintAdvantage(context.AttackChar, context.DefendingChar, wtb);
        _combatLog.PrintPreCombatLog(context.AttackChar, context.DefendingChar);
    }

    private void PerformAttacks(CombatContext context)
    {
        context.AttackChar.MarkHasInitiatedCombat();
        context.DefendingChar.MarkHasDefendedCombat();
        Attack(context, "first");

        if (!_attackHandler.CanCounterAttack(context.AttackChar, context.DefendingChar)) return;

        var counterContext = new CombatContext(context.Defender, context.Attacker, context.DefendingChar, context.AttackChar);
        Attack(counterContext, "first");

        context.AttackChar.ResetFirstAttackModifiers();
        context.DefendingChar.ResetFirstAttackModifiers();
    }

    private void Attack(CombatContext context, string attackType)
    {
        int damageDealt = CalculateDamage(context.AttackChar, context.DefendingChar, attackType);
        context.DamageDealt = damageDealt;
        
        _combatLog.DisplayAttackResult(context);

        context.AttackChar.MarkAsAttacked();
        context.DefendingChar.TakeDamage(damageDealt);

        HandleAttackHealingAndLog(context.AttackChar, damageDealt);
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

    private void HandleFollowUps(CombatContext context)
    {
        bool noFollowUps = true;

        if (CanFollowUp(context.AttackChar, context.DefendingChar))
        {
            Attack(context, "followup");
            noFollowUps = false;
        }

        if (CanFollowUp(context.DefendingChar, context.AttackChar))
        {
            var followUpContext = new CombatContext(context.Defender, context.Attacker, context.DefendingChar, context.AttackChar);
            Attack(followUpContext, "followup");
            noFollowUps = false;
        }

        if (noFollowUps)
            _combatLog.AnnounceNoFollowUps(context.AttackChar, context.DefendingChar);
    }

    private bool CanFollowUp(Character attacker, Character defender)
    {
        if (!attacker.IsAlive() || !defender.IsAlive()) return false;

        bool hasNegatedCounterAttack = attacker.CharacterModifiers.CombatModifiers.CounterAttackIsNegated &&
                                       !attacker.CharacterModifiers.CombatModifiers.NegatedCounterAttackNegation;
        if (hasNegatedCounterAttack) return false;

        int followUpCount = CountFollowUpPossibilities(attacker, defender);
        return followUpCount > 0;
    }

    private int CountFollowUpPossibilities(Character attacker, Character defender)
    {
        int count = 0;

        if (attacker.EffectiveSpd("followup") - defender.EffectiveSpd("followup") >= 5)
            count++;

        if (!attacker.CharacterModifiers.CombatModifiers.NegatedGuaranteedFollowup)
            count += attacker.CharacterModifiers.CombatModifiers.GuaranteedFollowupCounter;

        if (!attacker.CharacterModifiers.CombatModifiers.NegateNegatedFollowup)
            count -= attacker.CharacterModifiers.CombatModifiers.NegatedFollowupCounter;

        return count;
    }

    private void ApplyAfterCombatEffects(CombatContext context)
    {
        LoadAfterCombatHpChange(context.AttackChar);
        LoadAfterCombatHpChange(context.DefendingChar);
    }

    private void LoadAfterCombatHpChange(Character character)
    {
        if (character.IsAlive())
            character.ChangeHp();
    }

    private void PrintAfterCombatLogs(CombatContext context)
    {
        _combatLog.PrintPostCombatLog(context.AttackChar, context.DefendingChar);
        _combatLog.AnnounceResults(context.AttackChar, context.DefendingChar);
    }

    private void PostAttackCharacterUpdate(CombatContext context)
    {
        context.AttackChar.UpdateMostRecentOpponent(context.DefendingChar);
        context.DefendingChar.UpdateMostRecentOpponent(context.AttackChar);

        context.AttackChar.ResetModifiers();
        context.DefendingChar.ResetModifiers();
    }

    private void CheckAndRemoveDeadCharacter(Player player, Character character)
    {
        if (!character.IsAlive())
            player.RemoveCharacter(character);
    }

    private void PrintEndGameMessage()
    {
        Player winner = _playerHandler.IsAlive(_players[0]) ? _players[0] : _players[1];
        _combatLog.AnnounceWinner(winner);
    }
}
