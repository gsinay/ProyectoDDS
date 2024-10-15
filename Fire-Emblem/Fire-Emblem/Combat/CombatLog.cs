using Fire_Emblem_View;
using Fire_Emblem.Characters;

namespace Fire_Emblem.Combat
{
    public class CombatLog
    {
        private const double AdvantageWtb = 1.2;
        private const double DisadvantageWtb = 0.8;
        private readonly View _view;

        public CombatLog(View view)
        {
            _view = view;
        }

        public void AnnounceTurn(int turn, string characterName, int playerNumber)
        {
            _view.WriteLine($"Round {turn + 1}: {characterName} (Player {playerNumber}) comienza");
        }

        public void AnnounceOption(int playerNumber)
        {
            _view.WriteLine($"Player {playerNumber} selecciona una opción");
        }

        public void ListCharacters(Player player)
        {
            for (int i = 0; i < player.CharacterCount(); i++)
            {
                _view.WriteLine($"{i}: {player.GetCharacterName(i)}");
            }
        }

        public void PrintAdvantage(Character attacker, Character defender, double wtb)
        {
            if (wtb == AdvantageWtb)
                _view.WriteLine($"{attacker.Info.Name} ({attacker.Info.Weapon}) tiene ventaja con respecto a " +
                                $"{defender.Info.Name} ({defender.Info.Weapon})");
            else if (wtb == DisadvantageWtb)
                _view.WriteLine($"{defender.Info.Name} ({defender.Info.Weapon}) tiene ventaja con respecto a " +
                                $"{attacker.Info.Name} ({attacker.Info.Weapon})");
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
            PrintCombatBonuses(character);
            PrintFirstAttackBonuses(character);
            PrintFollowupBonuses(character);
            PrintCombatPenalties(character);
            PrintFirstAttackPenalties(character);
            PrintFollowupPenalties(character);
            PrintNeutralizedBonuses(character);
            PrintNeutralizedPenalties(character);
        }

        private void PrintCombatBonuses(Character character)
        {
            foreach (var stat in character.Stats.CombatBonuses.GetAllBonuses())
            {
                if (stat.Value != 0)
                {
                    _view.WriteLine($"{character.Info.Name} obtiene {stat.Key}+{stat.Value}");
                }
            }
        }

        private void PrintFirstAttackBonuses(Character character)
        {
            foreach (var stat in character.Stats.FirstAttackBonuses.GetAllBonuses())
            {
                if (stat.Value != 0)
                {
                    _view.WriteLine($"{character.Info.Name} obtiene {stat.Key}+{stat.Value} en su primer ataque");
                }
            }
        }

        private void PrintFollowupBonuses(Character character)
        {
            foreach (var stat in character.Stats.FollowupBonuses.GetAllBonuses())
            {
                if (stat.Value != 0)
                {
                    _view.WriteLine($"{character.Info.Name} obtiene {stat.Key}+{stat.Value} en su Follow-Up");
                }
            }
        }

        private void PrintCombatPenalties(Character character)
        {
            foreach (var stat in character.Stats.CombatPenalties.GetAllPenalties())
            {
                if (stat.Value != 0)
                {
                    _view.WriteLine($"{character.Info.Name} obtiene {stat.Key}-{stat.Value}");
                }
            }
        }

        private void PrintFirstAttackPenalties(Character character)
        {
            foreach (var stat in character.Stats.FirstAttackPenalties.GetAllPenalties())
            {
                if (stat.Value != 0)
                {
                    _view.WriteLine($"{character.Info.Name} obtiene {stat.Key}-{stat.Value} en su primer ataque");
                }
            }
        }

        private void PrintFollowupPenalties(Character character)
        {
            foreach (var stat in character.Stats.FollowupPenalties.GetAllPenalties())
            {
                if (stat.Value != 0)
                {
                    _view.WriteLine($"{character.Info.Name} obtiene {stat.Key}-{stat.Value} en su Follow-Up");
                }
            }
        }

        private void PrintNeutralizedBonuses(Character character)
        {
            foreach (var stat in character.Stats.NeutralizedBonuses.GetAllNeutralizations())
            {
                if (stat.Value)
                {
                    _view.WriteLine($"Los bonus de {stat.Key} de {character.Info.Name} fueron neutralizados");
                }
            }
        }

        private void PrintNeutralizedPenalties(Character character)
        {
            foreach (var stat in character.Stats.NeutralizedPenalties.GetAllNeutralizations())
            {
                if (stat.Value)
                {
                    _view.WriteLine($"Los penalty de {stat.Key} de {character.Info.Name} fueron neutralizados");
                }
            }
        }

        public void DisplayAttackResult(Character attacker, Character defender, int damage)
        {
            _view.WriteLine($"{attacker.Info.Name} ataca a {defender.Info.Name} con {damage} de daño");
        }

        public void AnnounceNoFollowUps()
        {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
        }

        public void AnnounceResults(Character attacker, Character defender)
        {
            _view.WriteLine($"{attacker.Info.Name} ({attacker.GetHp}) : {defender.Info.Name} ({defender.GetHp})");
        }

        public void AnnounceWinner(int playerNumber)
        {
            _view.WriteLine($"Player {playerNumber} ganó");
        }
    }
}
