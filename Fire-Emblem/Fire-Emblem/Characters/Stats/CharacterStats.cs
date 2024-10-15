namespace Fire_Emblem.Characters
{
    public class CharacterStats
    {
        public BaseStats BaseStats { get; }

        public StatBonuses CombatBonuses { get; }
        public StatBonuses FirstAttackBonuses { get; }
        public StatBonuses FollowupBonuses { get; }

        public StatPenalties CombatPenalties { get; }
        public StatPenalties FirstAttackPenalties { get; }
        public StatPenalties FollowupPenalties { get; }

        public StatNeutralizations NeutralizedBonuses { get; }
        public StatNeutralizations NeutralizedPenalties { get; }

        public CharacterStats(int hp, int atk, int spd, int def, int res)
        {
            BaseStats = new BaseStats(hp, atk, spd, def, res);

            CombatBonuses = new StatBonuses();
            FirstAttackBonuses = new StatBonuses();
            FollowupBonuses = new StatBonuses();

            CombatPenalties = new StatPenalties();
            FirstAttackPenalties = new StatPenalties();
            FollowupPenalties = new StatPenalties();

            NeutralizedBonuses = new StatNeutralizations();
            NeutralizedPenalties = new StatNeutralizations();
        }

        public void Reset()
        {
            CombatBonuses.Reset();
            FirstAttackBonuses.Reset();
            FollowupBonuses.Reset();

            CombatPenalties.Reset();
            FirstAttackPenalties.Reset();
            FollowupPenalties.Reset();

            NeutralizedBonuses.Reset();
            NeutralizedPenalties.Reset();
        }

        public void ResetFirstAttackModifiers()
        {
            FirstAttackBonuses.Reset();
            FirstAttackPenalties.Reset();
        }
    }
}
