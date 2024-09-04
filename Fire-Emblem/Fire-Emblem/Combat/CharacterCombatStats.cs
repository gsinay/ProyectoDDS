
namespace Fire_Emblem
{
    public class CharacterCombatStats
    {
        public Dictionary<string, int> StatBonuses { get; private set; }
        public Dictionary<string, int> StatPenalties { get; private set; }
        public Dictionary<string, int> FirstAttackBonuses { get; private set; }

        public CharacterCombatStats()
        {
            StatBonuses = InitializeStatDictionary();
            StatPenalties = InitializeStatDictionary();
            FirstAttackBonuses = InitializeStatDictionary();
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
            foreach (var key in StatBonuses.Keys.ToList())
            {
                StatBonuses[key] = 0;
                StatPenalties[key] = 0;
                FirstAttackBonuses[key] = 0;
            }
        }
    }
}