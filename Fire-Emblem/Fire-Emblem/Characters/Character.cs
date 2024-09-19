using Fire_Emblem.Combat;
using Fire_Emblem.Skills;
using Fire_Emblem.SkillsManager;

namespace Fire_Emblem.Characters
{
    public class Character
    {
      
        private readonly CharacterInfo _characterInfo;
        private CharacterStats _stats;
        private SkillList _skills;
        
        public bool IsInitiatingCombat { get; set; }
        public Character MostRecentOpponent { get; private set; }
        public bool HasAttacked { get; private set; }
        
        public Character(string name, WeaponName weapon, string gender, string deathQuote,
            int hp, int atk, int spd, int def, int res)
        {
            _characterInfo = new CharacterInfo(name, weapon, gender, deathQuote);
            _stats = new CharacterStats(hp, atk, spd, def, res);
            _skills = new SkillList();
        }
        
        public CharacterStats Stats => _stats;
        public CharacterInfo Info => _characterInfo;
        
        public int GetHp => _stats.BaseStats[StatName.Hp];
        public int GetMaxHp => _stats.BaseStats[StatName.MaxHp];
        public double GetRemainingHpPercentage() => (double)GetHp / GetMaxHp;

        public int EffectiveAtk => GetEffectiveStat(StatName.Atk);
        public int EffectiveSpd => GetEffectiveStat(StatName.Spd);
        public int EffectiveDef => GetEffectiveStat(StatName.Def);
        public int EffectiveRes => GetEffectiveStat(StatName.Res);
        
        public void AddSkill(ISkill skill)
        {
            _skills.AddSkill(skill);
        }

        public int SkillCount() => _skills.Count();

        public List<ISkill> GetSkills() => _skills.GetSkills();

        public void ApplyPermanentEffects()
        {
            foreach (var skill in _skills.GetSkills())
            {
                if (skill is OneTimeSkill permanentSkill)
                {
                    permanentSkill.ApplyEffect(this, null, null);
                }
            }
        }

        public void ApplySkillsBeforeCombat(Character opponent, CombatLog combatLog)
        {
            foreach (var skill in _skills.GetSkills())
            {
                skill.ApplyEffect(this, opponent, combatLog);
            }
        }

       
        public void IncreaseMaxHp(int amount)
        {
            _stats.BaseStats[StatName.Hp] += amount;
            _stats.BaseStats[StatName.MaxHp] += amount;
        }

        public bool IsAlive() => _stats.BaseStats[StatName.Hp] > 0;

        public void TakeDamage(int damage)
        {
            _stats.BaseStats[StatName.Hp] = Math.Max(0, GetHp - damage);
        }

    
        public void UpdateMostRecentOpponent(Character opponent)
        {
            MostRecentOpponent = opponent;
        }

        public void SetHasAttackedStatus()
        {
            HasAttacked = true;
        }

        public void ResetHasAttackStatus()
        {
            HasAttacked = false;
        }

        public void ResetModifiers()
        {
            _stats.Reset();
        }

        public void ResetFirstAttackModifiers()
        {
            _stats.ResetFirstAttackModifiers();
        }

      
        private int GetEffectiveStat(StatName stat)
        {
            int effectiveBonuses = _stats.NeutralizedBonuses[stat]
                ? 0
                : _stats.CombatBonuses[stat] + _stats.FirstAttackBonuses[stat] +
                  (HasAttacked ? _stats.FollowupBonuses[stat] : 0);

            int effectivePenalties = _stats.NeutralizedPenalties[stat]
                ? 0
                : _stats.CombatPenalties[stat] + _stats.FirstAttackPenalties[stat] +
                  (HasAttacked ? _stats.FollowupPenalties[stat] : 0);

            return Math.Max(0, _stats.BaseStats[stat] + effectiveBonuses + effectivePenalties);
        }
    }
}
