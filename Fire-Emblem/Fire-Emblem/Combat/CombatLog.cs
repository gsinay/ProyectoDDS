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

        public void AnnounceTurn(int turn, string characterName, int playerNumber)
        {
            _view.WriteLine($"Round {turn + 1}: {characterName} (Player {playerNumber}) comienza");
        }
        
        public void PrintAdvantage(Character attacker, Character defender, double WTB)
        {
           
            if (WTB == 1.2)
                _view.WriteLine($"{attacker.Info.Name} ({attacker.Info.Weapon}) tiene ventaja con respecto a {defender.Info.Name} " +
                                $"({defender.Info.Weapon})");
            else if(WTB == 0.8)
                _view.WriteLine($"{defender.Info.Name} ({defender.Info.Weapon}) tiene ventaja con respecto a {attacker.Info.Name} " +
                                $"({attacker.Info.Weapon})");
            else
            {
                _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
            }
        } 

        public void PrintLog(Character attacker, Character defender)
        {
            PrintCharacterLog(attacker);
            PrintCharacterLog(defender);
        }

        private void PrintCharacterLog(Character character)
        {
            PrintStats(character.Info.Name, character.Stats.CombatBonuses, "{0} obtiene {1}+{2}");
            PrintStats(character.Info.Name, character.Stats.FirstAttackBonuses,
                "{0} obtiene {1}+{2} en su primer ataque");
            PrintStats(character.Info.Name, character.Stats.FollowupBonuses,
                "{0} obtiene {1}+{2} en su Follow-Up");
            PrintStats(character.Info.Name, character.Stats.CombatPenalties, "{0} obtiene {1}{2}");
            PrintStats(character.Info.Name, character.Stats.FirstAttackPenalties,
                "{0} obtiene {1}{2} en su primer ataque");
            PrintStats(character.Info.Name, character.Stats.FollowupPenalties,
                "{0} obtiene {1}{2} en su Follow-Up");
            PrintNeutralization(character.Info.Name, character.Stats.NeutralizedBonuses,
                "Los bonus de {0} de {1} fueron neutralizados");
            PrintNeutralization(character.Info.Name, character.Stats.NeutralizedPenalties,
                "Los penalty de {0} de {1} fueron neutralizados");
        }

        private void PrintStats(string characterName, Dictionary<StatName, int> stats, string messageFormat)
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

        private void PrintNeutralization(string characterName, Dictionary<StatName, bool> neutralizedStats,
            string messageFormat)
        {
            foreach (var stat in neutralizedStats)
            {
                if (stat.Value)
                {
                    var printStatement = string.Format(messageFormat, stat.Key, characterName);
                    _view.WriteLine(printStatement);
                }
            }
        }

        public void AnnounceResults(Character attacker, Character defender)
        {
            _view.WriteLine($"{attacker.Info.Name} ({attacker.GetHP}) : {defender.Info.Name} ({defender.GetHP})");
        }
    }
}