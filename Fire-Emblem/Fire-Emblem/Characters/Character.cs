namespace Fire_Emblem.Characters
{
    public class Character
    {
        // Attributes "ultra base" del character. Son "publicos" pero por debajo no lo son.
        public string Name { get; private set; }
        public string Weapon { get; private set; }
        public string Gender { get; private set; }
        public string DeathQuote { get; private set; }

        private int _HP;
        private int _maxHP;
        private CharacterStats _stats;
        
        public CharacterStats Stats => _stats;
        
        // Properties necesarias para condiciones de distintas skills
        public bool IsInitiatingCombat { get; set; }
        public Character MostRecentOpponent { get; private set; }
        public bool HasAttacked { get; private set; }

        private SkillList _skills;

        public Character(string name, string weapon, string gender, string deathQuote, int hp, int atk, int spd, int def, int res)
        {
            Name = name;
            Weapon = weapon;
            Gender = gender;
            DeathQuote = deathQuote;
            _HP = hp;
            _maxHP = hp;
            _stats = new CharacterStats(atk, spd, def, res); 
            _skills = new SkillList();
        }

        public void AddSkill(ISkill skill)
        {
            _skills.AddSkill(skill);
        }

        public int SkillCount() => _skills.Count();

        public List<ISkill> GetSkills() => _skills.GetSkills();

        public bool IsAlive() => _HP > 0;

        public int GetHP => _HP;

        public int GetMaxHP => _maxHP;

        public void TakeDamage(int damage)
        {
            _HP = Math.Max(0, _HP - damage);
        }

        public int GetRemainingHpPercentage() => _HP  / _maxHP;

        // Get effective stats using the new dictionary-based structure in CharacterStats
        public int EffectiveAtk => _stats.GetEffectiveStat("Atk");
        public int EffectiveSpd => _stats.GetEffectiveStat("Spd");
        public int EffectiveDef => _stats.GetEffectiveStat("Def");
        public int EffectiveRes => _stats.GetEffectiveStat("Res");

        // Apply skills before combat
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
