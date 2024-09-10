using Fire_Emblem_View;
using Fire_Emblem.Characters;

namespace Fire_Emblem
{
    public class CombatLog
    {
        private View _view;

        // Constructor to initialize the View
        public CombatLog(View view)
        {
            _view = view;
        }

        public void PrintLog(Character attacker, Character defender)
        {
            PrintCharacterLog(attacker);
            PrintCharacterLog(defender);
        }

        private void PrintCharacterLog(Character character)
        {
            PrintStats(character.Name, character.Stats.CombatBonuses, "{0} obtiene {1}+{2}");
            PrintStats(character.Name, character.Stats.FirstAttackBonuses, 
                "{0} obtiene {1}+{2} en su primer ataque");
            PrintStats(character.Name, character.Stats.FollowupBonuses, 
                "{0} obtiene {1}+{2} en su Follow-up");
            PrintStats(character.Name, character.Stats.CombatPenalties, "{0} obtiene {1}{2}");
            PrintStats(character.Name, character.Stats.FirstAttackPenalties, 
                "{0} obtiene {1}{2} en su primer ataque");
            PrintStats(character.Name, character.Stats.FollowupPenalties, 
                "{0} obtiene {1}{2} en su Follow-up");
        }

        private void PrintStats(string characterName, Dictionary<string, int> stats, string messageFormat)
        {
            foreach (var stat in stats)
            {
                if (stat.Value != 0)
                {
                    var printStatement = string.Format(messageFormat, characterName, stat.Key, stat.Value);
                    _view.WriteLine(printStatement);
                }
            }
        }
    }
}