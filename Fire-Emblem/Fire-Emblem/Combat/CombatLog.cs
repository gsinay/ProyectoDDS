using Fire_Emblem_View;
using Fire_Emblem.Characters;

namespace Fire_Emblem
{
    public class CombatLog
    {
        private CharacterCombatStats _attackerStats;
        private CharacterCombatStats _defenderStats;

        private View _view;

        public CombatLog(View view)
        {
            _attackerStats = new CharacterCombatStats();
            _defenderStats = new CharacterCombatStats();
            _view = view;
        }

        public void LogBonus(Character character, string stat, int amount)
        {
            var stats = character.IsInitiatingCombat ? _attackerStats : _defenderStats;
            stats.StatBonuses[stat] += amount;
        }

        public void LogPenalty(Character character, string stat, int amount)
        {
            var stats = character.IsInitiatingCombat ? _attackerStats : _defenderStats;
            stats.StatPenalties[stat] += amount;
        }

        public void LogFirstAttackBonus(Character character, string stat, int amount)
        {
            var stats = character.IsInitiatingCombat ? _attackerStats : _defenderStats;
            stats.FirstAttackBonuses[stat] += amount;
        }

        public void PrintLog(Character attacker, Character defender)
        {
            PrintCharacterLog(attacker, _attackerStats);
            PrintCharacterLog(defender, _defenderStats);
            ResetStats();
        }

        private void PrintCharacterLog(Character character, CharacterCombatStats stats)
        {
            foreach (var stat in stats.StatBonuses.Keys)
            {
                if (stats.StatBonuses[stat] != 0)
                {
                    _view.WriteLine($"{character.Name} obtiene {stat}+{stats.StatBonuses[stat]}");
                }
            }

            foreach (var stat in stats.StatPenalties.Keys)
            {
                if (stats.StatPenalties[stat] != 0)
                {
                    _view.WriteLine($"{character.Name} obtiene {stat}-{stats.StatPenalties[stat]}");
                }
            }

            foreach (var stat in stats.FirstAttackBonuses.Keys)
            {
                if (stats.FirstAttackBonuses[stat] != 0)
                {
                    _view.WriteLine($"{character.Name} obtiene {stat}+{stats.FirstAttackBonuses[stat]} en su primer ataque");
                }
            }
        }

        private void ResetStats()
        {
            _attackerStats.Reset();
            _defenderStats.Reset();
        }
    }
}
