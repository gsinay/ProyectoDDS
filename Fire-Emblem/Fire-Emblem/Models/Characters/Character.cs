using Fire_Emblem.Models.Characters.Calculators;
using Fire_Emblem.Models.Characters.Modifiers;
using Fire_Emblem.Models.Characters.Stats;
using Fire_Emblem.Models.Collections;
using Fire_Emblem.Models.Names;
using Fire_Emblem.Models.Skills;


namespace Fire_Emblem.Models.Characters
{
    public class Character
    {

        private readonly CharacterInfo _characterInfo;
        private readonly CharacterStats _stats;
        private readonly SkillList _skills = new();
        private readonly CombatState _combatState = new();
        private readonly CharacterCombatModifiers _characterModifiers = new();
        private readonly DamageCalculator _damageCalculator = new();
        private readonly StatCalculator _statCalculator = new();
        
        private  bool _hasInitiatedCombat;
        private  bool _hasDefendedCombat;
        
        public Character(string name, WeaponName weapon, string gender, string deathQuote,
            int hp, int atk, int spd, int def, int res)
        {
            _characterInfo = new CharacterInfo(name, weapon, gender, deathQuote);
            _stats = new CharacterStats(hp, atk, spd, def, res);

        }
        public bool IsInitiatingCombat
        {
            get => _combatState.IsInitiatingCombat;
            set => _combatState.IsInitiatingCombat = value;
        }

        public Character? MostRecentOpponent => _combatState.MostRecentOpponent;
        public bool HasAttacked => _combatState.HasAttacked;

        public bool HasInitiatedCombat => _hasInitiatedCombat;
        public bool HasDefendedCombat => _hasDefendedCombat;

        public CharacterStats Stats => _stats;

        public CharacterCombatModifiers CharacterModifiers => _characterModifiers;
        public CharacterInfo Info => _characterInfo;


        public int GetHp => _stats.BaseStats.GetBaseStat(StatName.Hp);
        public int GetMaxHp => _stats.BaseStats.GetBaseStat(StatName.MaxHp);
        public double GetRemainingHpPercentage() => (double)GetHp / GetMaxHp;
        public List<ISkill> Skills => _skills.GetSkills();
        
        public int SkillCount() => _skills.Count();

        
        public int GeneralEffectiveAtk => _statCalculator.GetGeneralEffectiveStat(this, StatName.Atk);
        public int GeneralEffectiveSpd => _statCalculator.GetGeneralEffectiveStat(this, StatName.Spd);
        public int GeneralEffectiveDef => _statCalculator.GetGeneralEffectiveStat(this, StatName.Def);
        public int GeneralEffectiveRes => _statCalculator.GetGeneralEffectiveStat(this, StatName.Res);
        public int EffectiveAtk => _statCalculator.GetEffectiveStat(this, StatName.Atk);
        public int EffectiveSpd => _statCalculator.GetEffectiveStat(this, StatName.Spd);
        public int EffectiveDef => _statCalculator.GetEffectiveStat(this, StatName.Def);
        public int EffectiveRes => _statCalculator.GetEffectiveStat(this, StatName.Res);
        
        public void IncreaseMaxHp(int amount)
        {
            int currentHp = _stats.BaseStats.GetBaseStat(StatName.Hp);
            _stats.BaseStats.SetBaseStat(StatName.Hp, currentHp + amount);
            _stats.BaseStats.SetBaseStat(StatName.MaxHp, currentHp + amount);
        }

        public bool IsAlive() => _stats.BaseStats.GetBaseStat(StatName.Hp) > 0;

        public void TakeDamage(int damage)
        {
            int damageValue = Math.Max(0, GetHp - damage);
            _stats.BaseStats.SetBaseStat(StatName.Hp, damageValue);
        }


        public void UpdateMostRecentOpponent(Character opponent)
        {
            _combatState.UpdateMostRecentOpponent(opponent);
        }

        public void MarkAsAttacked()
        {
            _combatState.MarkAsAttacked();
        }

        public void MarkAsNotAttacked()
        {
            _combatState.MarkAsNotAttacked();
        }

        public void MarkHasInitiatedCombat()
        {
            _hasInitiatedCombat = true;
        }
        public void MarkHasDefendedCombat()
        {

            _hasDefendedCombat = true;
        }

        public void ResetModifiers()
        {
            _stats.Reset();
            _characterModifiers.Reset();
        }

        public void ResetFirstAttackModifiers()
        {
            _stats.ResetFirstAttackModifiers();
        }

        public int GetAttackWithReduction(int originalAttack)
        {
            return _damageCalculator.GetAttackWithReduction(this, originalAttack);
        }
        public  int GetAttackModifier()
        {
            return _damageCalculator.GetAttackModifier(this);
        }
        
    }
}
