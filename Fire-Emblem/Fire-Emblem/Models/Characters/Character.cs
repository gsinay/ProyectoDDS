using Fire_Emblem.Combat;
using Fire_Emblem.Models.Characters.Modifiers;
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
        private readonly CharacterCombatModifiers _characterModifiers;


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
            _characterModifiers = new CharacterCombatModifiers();

        }

        public CharacterStats Stats => _stats;

        public CharacterCombatModifiers CharacterModifiers => _characterModifiers;
        public CharacterInfo Info => _characterInfo;


        public int GetHp => _stats.BaseStats.GetBaseStat(StatName.Hp);
        public int GetMaxHp => _stats.BaseStats.GetBaseStat(StatName.MaxHp);
        public double GetRemainingHpPercentage() => (double)GetHp / GetMaxHp;

        
        public int GeneralEffectiveAtk => GetGeneralEffectiveStat(StatName.Atk);
        public int GeneralEffectiveSpd => GetGeneralEffectiveStat(StatName.Spd);
        public int GeneralEffectiveDef => GetGeneralEffectiveStat(StatName.Def);
        public int GeneralEffectiveRes => GetGeneralEffectiveStat(StatName.Res);
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

        public void ApplyBasicSkillsBeforeCombat(Character opponent)
        {
            foreach (var skill in _skills.GetSkills())
            {
                if (skill is BasicSkill basicSkill)
                    basicSkill.ApplySkill(this, opponent);
            }
        }

        public void ApplyModifierSkillsBeforeCombat(Character opponent)
        {
            foreach (var skill in _skills.GetSkills())
            {
                if (skill is ModifierSkill modifierSkill)
                {
                    modifierSkill.ApplySkill(this, opponent);
                }
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
            int attackAfterPercentageReduction = ApplyPercentageReduction(originalAttack);
            int totalAbsoluteReduction = CalculateTotalAbsoluteReduction();
            int finalAttack = ApplyAbsoluteReduction(attackAfterPercentageReduction, totalAbsoluteReduction);
            return Math.Max(0, finalAttack);
        }

        private int ApplyPercentageReduction(int originalAttack)
        {
            double cumulativePercentDamageReceived = CalculateCumulativePercentDamageReceived();
            double reducedAttack = Math.Round(originalAttack * cumulativePercentDamageReceived, 9);
            return (int)Math.Floor(reducedAttack);
        }

        private int CalculateTotalAbsoluteReduction()
        {
            int absoluteReduction = _characterModifiers.CombatModifiers.FlatDamageReduction;
            int additionalReduction = HasAttacked 
                ? _characterModifiers.FollowupModifiers.FlatDamageReduction
                : _characterModifiers.FirstAttackModifiers.FlatDamageReduction;
            return absoluteReduction + additionalReduction;
        }

        private int ApplyAbsoluteReduction(int attackValue, int totalAbsoluteReduction)
        {
            return attackValue - totalAbsoluteReduction;
        }

        private double CalculateCumulativePercentDamageReceived()
        {
            
            double percentDamageReceived = _characterModifiers.CombatModifiers.PercentDamageReceived;

         
            if (!HasAttacked)
                percentDamageReceived *= _characterModifiers.FirstAttackModifiers.PercentDamageReceived;
            
            else
             percentDamageReceived *= _characterModifiers.FollowupModifiers.PercentDamageReceived;

           
            return percentDamageReceived;
        }



        public  int GetAttackModifier()
        {
            int extraAttack = _characterModifiers.CombatModifiers.FlatAttackIncrement;
            if (!HasAttacked)
                extraAttack += _characterModifiers.FirstAttackModifiers.FlatAttackIncrement;
            extraAttack += _characterModifiers.FollowupModifiers.FlatAttackIncrement;
            return extraAttack;
        }
        
        private int GetGeneralEffectiveStat(StatName stat)
        {
            int effectiveBonuses = _stats.NeutralizedBonuses.IsNeutralized(stat)
                ? 0
                : _stats.CombatBonuses.GetBonus(stat);

            int effectivePenalties = _stats.NeutralizedPenalties.IsNeutralized(stat)
                ? 0
                : _stats.CombatPenalties.GetPenalty(stat);

            return Math.Max(0, _stats.BaseStats.GetBaseStat(stat) + effectiveBonuses - effectivePenalties);
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
                  (HasAttacked ? _stats.FollowupPenalties.GetPenalty(stat) : 0);

            return Math.Max(0, _stats.BaseStats.GetBaseStat(stat) + effectiveBonuses - effectivePenalties);
        }
        
        

        

    }
}
