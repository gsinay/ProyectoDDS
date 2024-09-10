namespace Fire_Emblem.Characters
{
    public class CharacterStats
    {
        
        public Dictionary<string, int> BaseStats;
        
        
        public Dictionary<string, int> CombatBonuses;
        public Dictionary<string, int> FirstAttackBonuses;
        public Dictionary<string, int> FollowupBonuses;
        
        public Dictionary<string, int> CombatPenalties;
        public Dictionary<string, int> FirstAttackPenalties;
        public Dictionary<string, int> FollowupPenalties;
        
        public CharacterStats(int atk, int spd, int def, int res)
        {
            BaseStats = new Dictionary<string, int>
            {
                { "Atk", atk },
                { "Spd", spd },
                { "Def", def },
                { "Res", res }
            };

           
            CombatBonuses = InitializeStatDictionary();
            FirstAttackBonuses = InitializeStatDictionary();
            FollowupBonuses = InitializeStatDictionary();
            
            CombatPenalties = InitializeStatDictionary();
            FirstAttackPenalties = InitializeStatDictionary();
            FollowupPenalties = InitializeStatDictionary();
        }
        
        private Dictionary<string, int> InitializeStatDictionary()
        {
            return new Dictionary<string, int>
            {
                { "Atk", 0 },
                { "Spd", 0 },
                { "Def", 0 },
                { "Res", 0 }
            };
        }
        
        public void Reset()
        {
            ResetDictionary(CombatBonuses);
            ResetDictionary(FirstAttackBonuses);
            ResetDictionary(FollowupBonuses);
            ResetDictionary(CombatPenalties);
            ResetDictionary(FirstAttackPenalties);
            ResetDictionary(FollowupPenalties);
        }

        public void ResetFirstAttackModifiers()
        {
            ResetDictionary(FirstAttackBonuses);
            ResetDictionary(FirstAttackPenalties);
        }

        private void ResetDictionary(Dictionary<string, int> statDictionary)
        {
            foreach (var stat in statDictionary.Keys.ToList())
            {
                statDictionary[stat] = 0;
            }
        }
        
        public int GetEffectiveStat(string stat)
        {
            return BaseStats[stat] +
                   CombatBonuses[stat] + FirstAttackBonuses[stat] + FollowupBonuses[stat] +
                   CombatPenalties[stat] + FirstAttackPenalties[stat] + FollowupPenalties[stat];
        }
    }
}
