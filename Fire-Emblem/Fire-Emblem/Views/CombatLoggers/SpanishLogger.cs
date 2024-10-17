
using Fire_Emblem_View;
using Fire_Emblem.Characters;
using Fire_Emblem.Logging;


namespace Fire_Emblem.Views
{
    public class SpanishLogger : AbstractLogger
    {
        public SpanishLogger(View view) : base(view)
        {
        }

        public override void AnnounceTurn(int turn, string characterName, int playerNumber)
        {
            _view.WriteLine($"Round {turn + 1}: {characterName} (Player {playerNumber}) comienza");
        }

        public override void AnnounceOption(int playerNumber)
        {
            _view.WriteLine($"Player {playerNumber} selecciona una opción");
        }

        public override void ListCharacters(Player player)
        {
            for (int i = 0; i < player.CharacterCount(); i++)
            {
                _view.WriteLine($"{i}: {player.GetCharacterName(i)}");
            }
        }

        public override void PrintAdvantage(Character attacker, Character defender, double wtb)
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

        protected override void PrintCombatBonuses(Character character)
        {
            foreach (var stat in character.Stats.CombatBonuses.GetAllBonuses())
            {
                if (stat.Value != 0)
                {
                    _view.WriteLine($"{character.Info.Name} obtiene {stat.Key}+{stat.Value}");
                }
            }
        }

        protected override void PrintFirstAttackBonuses(Character character)
        {
            foreach (var stat in character.Stats.FirstAttackBonuses.GetAllBonuses())
            {
                if (stat.Value != 0)
                {
                    _view.WriteLine($"{character.Info.Name} obtiene {stat.Key}+{stat.Value} en su primer ataque");
                }
            }
        }

        protected override void PrintFollowupBonuses(Character character)
        {
            foreach (var stat in character.Stats.FollowupBonuses.GetAllBonuses())
            {
                if (stat.Value != 0)
                {
                    _view.WriteLine($"{character.Info.Name} obtiene {stat.Key}+{stat.Value} en su Follow-Up");
                }
            }
        }

        protected override void PrintCombatPenalties(Character character)
        {
            foreach (var stat in character.Stats.CombatPenalties.GetAllPenalties())
            {
                if (stat.Value != 0)
                {
                    _view.WriteLine($"{character.Info.Name} obtiene {stat.Key}-{stat.Value}");
                }
            }
        }

        protected override void PrintFirstAttackPenalties(Character character)
        {
            foreach (var stat in character.Stats.FirstAttackPenalties.GetAllPenalties())
            {
                if (stat.Value != 0)
                {
                    _view.WriteLine($"{character.Info.Name} obtiene {stat.Key}-{stat.Value} en su primer ataque");
                }
            }
        }

        protected override void PrintFollowupPenalties(Character character)
        {
            foreach (var stat in character.Stats.FollowupPenalties.GetAllPenalties())
            {
                if (stat.Value != 0)
                {
                    _view.WriteLine($"{character.Info.Name} obtiene {stat.Key}-{stat.Value} en su Follow-Up");
                }
            }
        }

        protected override void PrintNeutralizedBonuses(Character character)
        {
            foreach (var stat in character.Stats.NeutralizedBonuses.GetAllNeutralizations())
            {
                if (stat.Value)
                {
                    _view.WriteLine($"Los bonus de {stat.Key} de {character.Info.Name} fueron neutralizados");
                }
            }
        }
        protected override void PrintNeutralizedPenalties(Character character)
        {
            foreach (var stat in character.Stats.NeutralizedPenalties.GetAllNeutralizations())
            {
                if (stat.Value)
                {
                    _view.WriteLine($"Los penalty de {stat.Key} de {character.Info.Name} fueron neutralizados");
                }
            }
        }
        
        protected override void PrintCombatFlatAttackIncrement(Character character)
        {
            int increment = character.CharacterModifiers.CombatModifiers.FlatAttackIncrement;
            if (increment > 0)
                _view.WriteLine($"{character.Info.Name} realizará +{increment} daño extra en cada ataque");
            
        }
        protected override void PrintFirstAttackFlatAttackIncrement(Character character)
        {
            int increment = character.CharacterModifiers.FirstAttackModifiers.FlatAttackIncrement;
            if (increment > 0)
                _view.WriteLine($"{character.Info.Name} realizará +{increment} daño extra en su primer ataque");
            
        }
        
        protected override void PrintFollowupFlatAttackIncrement(Character character)
        {
            int increment = character.CharacterModifiers.FollowupModifiers.FlatAttackIncrement;
            if (increment > 0)
                _view.WriteLine($"{character.Info.Name} realizará +{increment} daño extra en su Follow-Up");
            
        }

        protected override void PrintCombatPercentualDamageReduction(Character character)
        {
            if(character.CharacterModifiers.CombatModifiers.PercentDamageReceived < 1)
            {
                double damageReductionPercent = 1.0 - character.CharacterModifiers.CombatModifiers.PercentDamageReceived;
                int damageReduction = Convert.ToInt32(damageReductionPercent * 100);
                
                _view.WriteLine($"{character.Info.Name} reducirá el daño de los ataques del rival en un {damageReduction}%");
            }
        }
        
        protected override void PrintFirstAttackPercentualDamageReduction(Character character)
        {
            if(character.CharacterModifiers.FirstAttackModifiers.PercentDamageReceived < 1)
            {
                double damageReductionPercent = 1.0 - character.CharacterModifiers.FirstAttackModifiers.PercentDamageReceived;
                int damageReduction = Convert.ToInt32(damageReductionPercent * 100);
                
                _view.WriteLine($"{character.Info.Name} reducirá el daño del primer ataque" +
                                $" del rival en un {damageReduction}%");
            }
        }

        protected override void PrintFollowupAttackPercentualDamageReduction(Character character)
        {
            if(character.CharacterModifiers.FollowupModifiers.PercentDamageReceived < 1)
            {
                double damageReductionPercent = 1.0 - character.CharacterModifiers.FollowupModifiers.PercentDamageReceived;
                int damageReduction = Convert.ToInt32(damageReductionPercent * 100);
                
                _view.WriteLine($"{character.Info.Name} reducirá el " +
                                $"daño del Follow-Up del rival en un {damageReduction}%");
            }
        }
        
        protected override void PrintCombatFlatDamageReduction(Character character)
        {
            if(character.CharacterModifiers.CombatModifiers.FlatDamageReduction > 0)
            {
                int amount = character.CharacterModifiers.CombatModifiers.FlatDamageReduction;
                _view.WriteLine($"{character.Info.Name} recibirá -{amount} daño en cada ataque");
            }
        }

        public override void DisplayAttackResult(Character attacker, Character defender, int damage)
        {
            _view.WriteLine($"{attacker.Info.Name} ataca a {defender.Info.Name} con {damage} de daño");
        }

        public override void AnnounceNoFollowUps()
        {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
        }

        public override void AnnounceResults(Character attacker, Character defender)
        {
            _view.WriteLine($"{attacker.Info.Name} ({attacker.GetHp}) : {defender.Info.Name} ({defender.GetHp})");
        }

        public override void AnnounceWinner(int playerNumber)
        {
            _view.WriteLine($"Player {playerNumber} ganó");
        }
    }
}
