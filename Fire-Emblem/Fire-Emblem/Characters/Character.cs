using Fire_Emblem.Combat;
using Fire_Emblem.Skills;
using Fire_Emblem.SkillsManager;

namespace Fire_Emblem.Characters
{
    public class Character
    {
      
        private readonly CharacterInfo _characterInfo;
        private readonly CharacterStats _stats;
        private readonly SkillList _skills;
        private readonly CombatState _combatState;

        public bool IsInitiatingCombat
        {
            get => _combatState.IsInitiatingCombat;
            set => _combatState.IsInitiatingCombat = value;
        }
        public Character? MostRecentOpponent => _combatState.MostRecentOpponent;
        public bool HasAttacked => _combatState.HasAttacked;

        public Character(string name, WeaponName weapon, string gender, string deathQuote,
            int hp, int atk, int spd, int def, int res)
        {
            _characterInfo = new CharacterInfo(name, weapon, gender, deathQuote);
            _stats = new CharacterStats(hp, atk, spd, def, res);
            _skills = new SkillList();
            _combatState = new CombatState();
        }
        
        public CharacterStats Stats => _stats;
        public CharacterInfo Info => _characterInfo;
        
        public int GetHp => _stats.BaseStats.GetBaseStat(StatName.Hp);
        public int GetMaxHp => _stats.BaseStats.GetBaseStat(StatName.MaxHp);
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
                    permanentSkill.ApplySkill(this, null!);
                }
            }
        }

        public void ApplySkillsBeforeCombat(Character opponent, CombatLog combatLog)
        {
            foreach (var skill in _skills.GetSkills())
            {
                skill.ApplySkill(this, opponent);
            }
        }

       
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
            _combatState.UpdateMostRecentOpponent(opponent);        }

        public void MarkAsAttacked()
        {
            _combatState.MarkAsAttacked();
        }

        public void MarkAsNotAttacked()
        {
            _combatState.MarkAsNotAttacked();
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
            int effectiveBonuses = _stats.NeutralizedBonuses.IsNeutralized(stat)
                ? 0
                : _stats.CombatBonuses.GetBonus(stat) + _stats.FirstAttackBonuses.GetBonus(stat) +
                  (HasAttacked ? _stats.FollowupBonuses.GetBonus(stat) : 0);

            int effectivePenalties = _stats.NeutralizedPenalties.IsNeutralized(stat)
                ? 0
                : _stats.CombatPenalties.GetPenalty(stat) + _stats.FirstAttackPenalties.GetPenalty(stat) +
                  (HasAttacked ? _stats.FollowupPenalties.GetPenalty(stat): 0);

            return Math.Max(0, _stats.BaseStats.GetBaseStat(stat) + effectiveBonuses - effectivePenalties);
        }
    }
}
