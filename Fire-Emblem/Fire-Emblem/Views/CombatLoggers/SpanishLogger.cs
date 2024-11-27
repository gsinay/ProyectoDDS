using Fire_Emblem_View;
using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Player;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Views.CombatLoggers
{
    public class SpanishLogger : ILogger
    {
        private const double AdvantageWtb = 1.2;
        private const double DisadvantageWtb = 0.8;
        private readonly View _view;

        public SpanishLogger(View view)
        {
            _view = view;
        }

        public int GetUserFileInput()
        {
            return Convert.ToInt32(_view.ReadLine());
        }
       
        public void AskForFile()
        {
            _view.WriteLine("Elige un archivo para cargar los equipos");
        }

        public void PrintNoFileFound()
        {
            _view.WriteLine("No se encontraron archivos de equipos.");
        }

        public void PrintFileNumberAndName(int number, string fileName)
        {
            _view.WriteLine($"{number}: {fileName}");
        }

        public void PrintGameNotValid()
        {
            _view.WriteLine("Archivo de equipos no válido");
        }

        public string[] GetTeamsFromUser()
        {
            return [];
            
        }
        
        public void UpdateTeams(Player attacker, Player defender)
        {
           
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
        
        public int GetUserCharacterInput(Player player)
        {
            return Convert.ToInt32(_view.ReadLine());
        }

        public void PrintAdvantage(Character attacker, Character defender, double wtb)
        {
            if (wtb == AdvantageWtb)
            {
                _view.WriteLine($"{attacker.Info.Name} ({attacker.Info.Weapon}) tiene ventaja con respecto a {defender.Info.Name} ({defender.Info.Weapon})");
            }
            else if (wtb == DisadvantageWtb)
            {
                _view.WriteLine($"{defender.Info.Name} ({defender.Info.Weapon}) tiene ventaja con respecto a {attacker.Info.Name} ({attacker.Info.Weapon})");
            }
            else
            {
                _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
            }
        }

        public void DisplayAttackResult(Character attacker, Character defender, int damage)
        {
            _view.WriteLine($"{attacker.Info.Name} ataca a {defender.Info.Name} con {damage} de daño");
        }
        
        public void DisplayHealingResult(Character attacker, int healing)
        {
            if (healing > 0)
            {
                _view.WriteLine($"{attacker.Info.Name} recupera {healing} HP luego de atacar y queda con " +
                                $"{attacker.Stats.BaseStats.GetBaseStat(StatName.Hp)} HP.");
            }
        }

        public void AnnounceNoFollowUps(Character attacker, Character defender)
        {
            if (defender.CharacterModifiers.CombatModifiers.CounterAttackIsNegated)
                _view.WriteLine($"{attacker.Info.Name} no puede hacer un follow up");
            else
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

        public void PrintPreCombatLog(Character attacker, Character defender)
        {
            PrintCharacterPreCombatLog(attacker);
            PrintCharacterPreCombatLog(defender);
            PrintCharacterChangesBeforeCombat(attacker);
            PrintCharacterChangesBeforeCombat(defender);
        }
        
        private void PrintCharacterChangesBeforeCombat(Character character)
        {
            PrintHpLostBeforeCombat(character);
        }
        
        private void PrintHpLostBeforeCombat(Character character)
        {
            int hpLost = character.CharacterModifiers.CombatModifiers.BeforeCombatHpReduction;
            if (hpLost > 0)
                _view.WriteLine($"{character.Info.Name} recibe {hpLost} de daño antes de iniciar el combate " +
                                $"y queda con {character.GetHp} HP");
        }
        private void PrintCharacterPreCombatLog(Character character)
        {
            PrintCombatBonuses(character);
            PrintFirstAttackBonuses(character);
            PrintFollowupBonuses(character);
            PrintCombatPenalties(character);
            PrintFirstAttackPenalties(character);
            PrintFollowupPenalties(character);
            PrintNeutralizedBonuses(character);
            PrintNeutralizedPenalties(character);
            PrintCombatFlatAttackIncrement(character);
            PrintFirstAttackFlatAttackIncrement(character);
            PrintFollowupFlatAttackIncrement(character);
            PrintCombatPercentualDamageReduction(character);
            PrintFirstAttackPercentualDamageReduction(character);
            PrintFollowupAttackPercentualDamageReduction(character);
            PrintCombatFlatDamageReduction(character);
            PrintHealingForEachAttack(character);
            PrintHasNegatedCounterAttack(character);
            PrintGuaranteedFollowupCounter(character);
            PrintNegatedFollowupCounter(character);
            PrintImmuneToFollowupNegation(character);
            PrintImmuneToFollowupGuaranteed(character);

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

        private void PrintCombatFlatAttackIncrement(Character character)
        {
            int increment = character.CharacterModifiers.CombatModifiers.FlatAttackIncrement;
            if (increment > 0)
                _view.WriteLine($"{character.Info.Name} realizará +{increment} daño extra en cada ataque");
        }

        private void PrintFirstAttackFlatAttackIncrement(Character character)
        {
            int increment = character.CharacterModifiers.FirstAttackModifiers.FlatAttackIncrement;
            if (increment > 0)
                _view.WriteLine($"{character.Info.Name} realizará +{increment} daño extra en su primer ataque");
        }

        private void PrintFollowupFlatAttackIncrement(Character character)
        {
            int increment = character.CharacterModifiers.FollowupModifiers.FlatAttackIncrement;
            if (increment > 0)
                _view.WriteLine($"{character.Info.Name} realizará +{increment} daño extra en su Follow-Up");
        }

        private void PrintCombatPercentualDamageReduction(Character character)
        {
            if (character.CharacterModifiers.CombatModifiers.PercentDamageReceived < 1)
            {
                double damageReductionPercent = 1.0 - character.CharacterModifiers.CombatModifiers.PercentDamageReceived;
                int damageReduction = Convert.ToInt32(damageReductionPercent * 100);

                _view.WriteLine($"{character.Info.Name} reducirá el daño de los ataques del rival en un {damageReduction}%");
            }
        }

        private void PrintFirstAttackPercentualDamageReduction(Character character)
        {
            if (character.CharacterModifiers.FirstAttackModifiers.PercentDamageReceived < 1)
            {
                double damageReductionPercent = 1.0 - character.CharacterModifiers.FirstAttackModifiers.PercentDamageReceived;
                int damageReduction = Convert.ToInt32(damageReductionPercent * 100);

                _view.WriteLine($"{character.Info.Name} reducirá el daño del primer ataque del rival en un {damageReduction}%");
            }
        }

        private void PrintFollowupAttackPercentualDamageReduction(Character character)
        {
            if (character.CharacterModifiers.FollowupModifiers.PercentDamageReceived < 1)
            {
                double damageReductionPercent = 1.0 - character.CharacterModifiers.FollowupModifiers.PercentDamageReceived;
                int damageReduction = Convert.ToInt32(damageReductionPercent * 100);

                _view.WriteLine($"{character.Info.Name} reducirá el daño del Follow-Up del rival en un {damageReduction}%");
            }
        }

        private void PrintCombatFlatDamageReduction(Character character)
        {
            if (character.CharacterModifiers.CombatModifiers.FlatDamageReduction > 0)
            {
                int amount = character.CharacterModifiers.CombatModifiers.FlatDamageReduction;
                _view.WriteLine($"{character.Info.Name} recibirá -{amount} daño en cada ataque");
            }
        }
        private void PrintHealingForEachAttack(Character character)
        {
            if (character.CharacterModifiers.CombatModifiers.PercentHealingReceivedAfterAttack > 0)
            {
                double healingPercent = character.CharacterModifiers.CombatModifiers.PercentHealingReceivedAfterAttack;
                int healing = Convert.ToInt32(healingPercent * 100);
                _view.WriteLine($"{character.Info.Name} recuperará HP igual al {healing}% del daño " +
                                $"realizado en cada ataque");
            }
        }
        private void PrintHasNegatedCounterAttack(Character character)
        {
            if (character.CharacterModifiers.CombatModifiers.CounterAttackIsNegated)
            {
                if (character.CharacterModifiers.CombatModifiers.NegatedCounterAttackNegation)
                    _view.WriteLine($"{character.Info.Name} neutraliza los efectos que previenen sus contraataques");
                else
                    _view.WriteLine($"{character.Info.Name} no podrá contraatacar");
            }
        }


        private void PrintGuaranteedFollowupCounter(Character character)
        {
            int count = character.CharacterModifiers.CombatModifiers.GuaranteedFollowupCounter;
            if (count > 0)
                _view.WriteLine($"{character.Info.Name} tiene {count} efecto(s) " +
                                $"que garantiza(n) su follow up activo(s)");
        }
        
        private void PrintNegatedFollowupCounter(Character character)
        {
            int count = character.CharacterModifiers.CombatModifiers.NegatedFollowupCounter;
            if (count > 0)
                _view.WriteLine($"{character.Info.Name} tiene {count} efecto(s)" +
                                $" que neutraliza(n) su follow up activo(s)");
        }

        private void PrintImmuneToFollowupNegation(Character character)
        {
            if (character.CharacterModifiers.CombatModifiers.NegateNegatedFollowup)
            {
                _view.WriteLine($"{character.Info.Name} es inmune a los efectos que neutralizan su follow up");
            }
        }
        
        private void PrintImmuneToFollowupGuaranteed(Character character)
        {
            if (character.CharacterModifiers.CombatModifiers.NegatedGuaranteedFollowup)
            {
                _view.WriteLine($"{character.Info.Name} es inmune a los efectos que garantizan su follow up");
            }
        }

        
        

        public void PrintPostCombatLog(Character attacker, Character defender)
        {
            PrintCharacterAfterCombatLog(attacker);
            PrintCharacterAfterCombatLog(defender);
        }

        private void PrintCharacterAfterCombatLog(Character character)
        {
            PrintHpChangedAfterCombat(character);
        }
        private void PrintHpChangedAfterCombat(Character character)
        {
            int hpChange = character.GetHpChange();
            if (hpChange < 0 && character.IsAlive())
                _view.WriteLine($"{character.Info.Name} recibe {-hpChange} de daño despues del combate");
            else if(hpChange > 0 && character.IsAlive())
                _view.WriteLine($"{character.Info.Name} recupera {hpChange} HP despues del combate");
        }
        
    }
}
