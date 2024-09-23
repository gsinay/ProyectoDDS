namespace Fire_Emblem.Characters
{
    public class CharacterStats
    {

        public Dictionary<StatName, int> BaseStats;


        public Dictionary<StatName, int> CombatBonuses;
        public Dictionary<StatName, int> FirstAttackBonuses;
        public Dictionary<StatName, int> FollowupBonuses;

        public Dictionary<StatName, int> CombatPenalties;
        public Dictionary<StatName, int> FirstAttackPenalties;
        public Dictionary<StatName, int> FollowupPenalties;

        public Dictionary<StatName, bool> NeutralizedBonuses;
        public Dictionary<StatName, bool> NeutralizedPenalties;


        public CharacterStats(int hp, int atk, int spd, int def, int res)
        {
            BaseStats = new Dictionary<StatName, int>
            {
                {StatName.Hp, hp},
                {StatName.MaxHp, hp},
                { StatName.Atk, atk },
                { StatName.Spd, spd },
                { StatName.Def, def },
                { StatName.Res, res }
            };


            CombatBonuses = InitializeStatDictionary();
            FirstAttackBonuses = InitializeStatDictionary();
            FollowupBonuses = InitializeStatDictionary();

            CombatPenalties = InitializeStatDictionary();
            FirstAttackPenalties = InitializeStatDictionary();
            FollowupPenalties = InitializeStatDictionary();

            NeutralizedBonuses = InitializeNeutralizationDictionary();
            NeutralizedPenalties = InitializeNeutralizationDictionary();
        }

        private Dictionary<StatName, int> InitializeStatDictionary()
        {
            return new Dictionary<StatName, int>
            {
                { StatName.Atk, 0 },
                { StatName.Spd, 0 },
                { StatName.Def, 0 },
                { StatName.Res, 0 }
            };
        }

        private Dictionary<StatName, bool> InitializeNeutralizationDictionary()
        {
            return new Dictionary<StatName, bool>
            {
                { StatName.Atk, false },
                { StatName.Spd, false },
                { StatName.Def, false },
                { StatName.Res, false }
            };
        }


        public void Reset()
        {
            CombatBonuses = InitializeStatDictionary();
            FirstAttackBonuses = InitializeStatDictionary();
            FollowupBonuses = InitializeStatDictionary();

            CombatPenalties = InitializeStatDictionary();
            FirstAttackPenalties = InitializeStatDictionary();
            FollowupPenalties = InitializeStatDictionary();

            NeutralizedBonuses = InitializeNeutralizationDictionary();
            NeutralizedPenalties = InitializeNeutralizationDictionary();
        }

        public void ResetFirstAttackModifiers()
        {
            FirstAttackBonuses = InitializeStatDictionary();
            FirstAttackPenalties = InitializeStatDictionary();

        }
    }
}
