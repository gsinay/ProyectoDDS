namespace Fire_Emblem.Characters
{
    public class Character
    {
        // Los siguientes son atributos "base" del character
        public string Name { get; set; }
        
        public string Weapon { get; set; }
        public string Gender { get; set; } 
        public string DeathQuote { get; set; }
        
        private int _HP;
        private int _maxHP;

        public int Atk { get; set; }
        public int Spd { get; set; }
        public int Def { get; set; }
        public int Res { get; set; }
        
        // Modifiers para skills por turno
        public int AtkModifier { get;  set; }
        public int SpdModifier { get; set; }
        public int DefModifier { get; set; }
        public int ResModifier { get; set; }
        
        //Modifiers para skills que son para unicamente primer ataque:
        public int FirstAttackAtkModifier { get;  set; }
        public int FirstAttackSpdModifier { get; set; }
        public int FirstAttackDefModifier { get; set; }
        public int FirstAttackResModifier { get; set; }
        
        // properties necesarias para condiciones de distintas skills
        public bool IsInitiatingCombat { get; set; }
        public Character MostRecentOpponent { get; private set; }
        
        public bool HasAttacked { get; private set; }



        private SkillList _Skills;

        public Character()
        {
            _Skills = new SkillList();
            AtkModifier = 0;
            SpdModifier = 0;
            DefModifier = 0;
            ResModifier = 0;
        }

        public Character(string name, string weapon, string gender,
            string deathQuote, int hp, int atk, int spd, int def, int res)
        {
            Name = name;
            Weapon = weapon;
            Gender = gender;
            DeathQuote = deathQuote;
            _HP = hp;
            _maxHP = hp;
            Atk = atk;
            Spd = spd;
            Def = def;
            Res = res;
            AtkModifier = 0;
            SpdModifier = 0;
            DefModifier = 0;
            ResModifier = 0;
            _Skills = new SkillList();
        }

        public void SetHP(int hp)
        {
            _HP = Math.Max(0, hp);
            _maxHP = Math.Max(0, hp);
        }

        public void AddSkill(ISkill skill)
        {
            _Skills.AddSkill(skill);
        }

        public int SkillCount() => _Skills.Count();

        public List<ISkill> GetSkills() => _Skills.GetSkills();

        public bool IsAlive() => _HP > 0;

        public int GetHP => _HP;

        public int GetMaxHP => _maxHP;

        public void TakeDamage(int damage)
        {
            _HP = Math.Max(0, _HP - damage);
        }

        public int GetRemainingHpPercentage() => _HP / _maxHP;

        // properties de stats efectivas
        public int EffectiveAtk => Atk + AtkModifier + FirstAttackAtkModifier;
        public int EffectiveSpd => Spd + SpdModifier + FirstAttackSpdModifier;
        public int EffectiveDef => Def + DefModifier + FirstAttackDefModifier;
        public int EffectiveRes => Res + ResModifier + FirstAttackResModifier;

        public void ApplySkillsBeforeCombat(Character opponent, CombatLog combatLog)
        {
            foreach (var skill in _Skills.GetSkills())
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
            AtkModifier = 0;
            SpdModifier = 0;
            DefModifier = 0;
            ResModifier = 0;
        }

        public void ResetFirstAttackModifiers()
        {
            FirstAttackAtkModifier = 0;
            FirstAttackSpdModifier = 0;
            FirstAttackDefModifier = 0;
            FirstAttackResModifier = 0;
        }
        
    }
}
