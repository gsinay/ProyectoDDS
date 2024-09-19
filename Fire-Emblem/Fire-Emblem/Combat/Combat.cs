using Fire_Emblem_View;
using Fire_Emblem.Characters;

namespace Fire_Emblem.Combat;

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
        if (HasAdvantage(attacker, defender)) return 1.2; 
        if (HasDisadvantage(attacker, defender)) return 0.8;
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
        attacker.SetHasAttackedStatus();
        defender.SetHasAttackedStatus();
        attacker.ResetFirstAttackModifiers();
        defender.ResetFirstAttackModifiers();
        if (!attacker.IsAlive()) return;
        HandleFollowUps(attacker, defender);
    }
    private void Attack(Character attacker, Character defender)
    {
        double WTB = CheckTriangleAdvantage(attacker, defender);
        int effectiveDefense = GetEffectiveDefense(attacker, defender);
        int attackPower = CalculateAttackPower(attacker, WTB);
        int damage = CalculateDamage(attackPower, effectiveDefense);

        defender.TakeDamage(damage);
        _combatlog.DisplayAttackResult(attacker, defender, damage);
    }
    
    private int CalculateAttackPower(Character attacker, double WTB)
    {
        return (int)(attacker.EffectiveAtk * WTB);
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
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
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
        Player winner = Players[0].IsAlive() ? Players[0] : Players[1];
        _view.WriteLine($"Player {winner.PlayerNumber} ganó");
    }
    
}