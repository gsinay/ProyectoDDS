using Fire_Emblem.Skills.Effects;

namespace Fire_Emblem.Characters
{
    public class Character
    {

        private readonly CharacterInfo _characterInfo;
        private int _HP;
        private int _maxHP;
        private CharacterStats _stats;
        
      
        public CharacterStats Stats => _stats;
        public CharacterInfo Info => _characterInfo;


        public bool IsInitiatingCombat { get; set; }
        public Character MostRecentOpponent { get; private set; }
        public bool HasAttacked { get; private set; }
        
        private SkillList _skills;

        public Character(string name, string weapon, string gender, string deathQuote, int hp, int atk, int spd, int def, int res)
        {
            _characterInfo = new CharacterInfo(name, weapon, gender, deathQuote);
            _HP = hp;
            _maxHP = hp;
            _stats = new CharacterStats(hp, atk, spd, def, res); 
            _skills = new SkillList();
        }

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
        public void IncreaseMaxHP(int amount)
        {
            _HP += amount;
            _maxHP = _HP; 
            
        }

        public bool IsAlive() => _HP > 0;

        public int GetHP => _HP;

        public int GetMaxHP => _maxHP;

        public void TakeDamage(int damage)
        {
            _HP = Math.Max(0, _HP - damage);
        }

        public double GetRemainingHpPercentage() => (double)_HP / _maxHP;
        
        
        public int EffectiveAtk => _stats.GetEffectiveStat(StatName.Atk, HasAttacked);
        public int EffectiveSpd => _stats.GetEffectiveStat(StatName.Spd, HasAttacked);
        public int EffectiveDef => _stats.GetEffectiveStat(StatName.Def, HasAttacked);
        public int EffectiveRes => _stats.GetEffectiveStat(StatName.Res, HasAttacked);

       
        public void ApplySkillsBeforeCombat(Character opponent, CombatLog combatLog)
        {
            foreach (var skill in _skills.GetSkills())
            {
                skill.ApplyEffect(this, opponent, combatLog);
            }
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
    }
}
