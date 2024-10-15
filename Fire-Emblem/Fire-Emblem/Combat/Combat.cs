using Fire_Emblem_View;
using Fire_Emblem.Characters;


namespace Fire_Emblem.Combat;

public class Combat
{
    private const double _AdvantageWTB = 1.2;
    private const double _DisadvantageWTB = 0.8;
    private readonly Player[] _players;
    private int _turn;

    private readonly View _view;

    private readonly CombatLog _combatlog;

    public Combat(View view, Setup setup)
    {
        _players = setup.Players;
        _turn = 0;
        _view = view;
        _combatlog = new CombatLog(_view);
        
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
        character.MarkAsNotAttacked();
        return character;

    }

    private Character SelectCharacter(Player player)
    {
        _combatlog.AnnounceOption(player.PlayerNumber);
        _combatlog.ListCharacters(player);
        int selection = Convert.ToInt32(_view.ReadLine());
        return player.Characters[selection];

    }
    

    private void PrintPreAttackLogs(Player attacker, Character attackChar, Character defendingChar)
    {
        _combatlog.AnnounceTurn(_turn, attackChar.Info.Name, attacker.PlayerNumber);
        double wtb = GetTriangleAdvantage(attackChar, defendingChar);
        _combatlog.PrintAdvantage(attackChar, defendingChar, wtb);
        _combatlog.PrintLog(attackChar, defendingChar);
    }
    
    private double GetTriangleAdvantage(Character attacker, Character defender)
    {
        if (HasAdvantage(attacker, defender)) return _AdvantageWTB; 
        if (HasDisadvantage(attacker, defender)) return _DisadvantageWTB;
        return 1;
    }
    
    private bool HasAdvantage(Character attacker, Character defender)
    {
        return GetAdvantagedWeapon(attacker.Info.Weapon) == defender.Info.Weapon;
    }

    private bool HasDisadvantage(Character attacker, Character defender)
    {
        return GetAdvantagedWeapon(defender.Info.Weapon) == attacker.Info.Weapon;
    }
    
    private WeaponName GetAdvantagedWeapon(WeaponName weapon)
    {
        return weapon switch
        {
            WeaponName.Sword => WeaponName.Axe,
            WeaponName.Axe => WeaponName.Lance,
            WeaponName.Lance => WeaponName.Sword,
            _ => WeaponName.None
        };
    }
    private void PerformAttacks(Character attacker, Character defender)
    {
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
        double wtb = GetTriangleAdvantage(attacker, defender);
        int effectiveDefense = GetEffectiveDefense(attacker, defender);
        int attackPower = CalculateAttackPower(attacker, wtb);
        int damage = CalculateDamage(attackPower, effectiveDefense);

        defender.TakeDamage(damage);
        _combatlog.DisplayAttackResult(attacker, defender, damage);
    }
    
    private int CalculateAttackPower(Character attacker, double wtb)
    {
        return (int)(attacker.EffectiveAtk * wtb);
    }
    
    private int CalculateDamage(int attackPower, int effectiveDefense)
    {
        int rawDamage = attackPower - effectiveDefense;
        return Math.Max(0, rawDamage);
    }


    
    private int GetEffectiveDefense(Character attacker, Character defender)
    {
        if (attacker.Info.Weapon == WeaponName.Magic)
            return defender.EffectiveRes;
        return defender.EffectiveDef;
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