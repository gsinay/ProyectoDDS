using Fire_Emblem_View;
using Fire_Emblem.Characters;


namespace Fire_Emblem.Logging
{
    public abstract class AbstractLogger
    {
        protected const double AdvantageWtb = 1.2;
        protected const double DisadvantageWtb = 0.8;
        protected readonly View _view;

        public AbstractLogger(View view)
        {
            _view = view;
        }
        
        public string  GetUserInput()
        {
            return _view.ReadLine();
        }

        
        public abstract void AnnounceTurn(int turn, string characterName, int playerNumber);
        public abstract void AnnounceOption(int playerNumber);
        public abstract void ListCharacters(Player player);
        public abstract void PrintAdvantage(Character attacker, Character defender, double wtb);
        public abstract void DisplayAttackResult(Character attacker, Character defender, int damage);
        public abstract void AnnounceNoFollowUps();
        public abstract void AnnounceResults(Character attacker, Character defender);
        public abstract void AnnounceWinner(int playerNumber);

       
        public void PrintLog(Character attacker, Character defender)
        {
            PrintCharacterLog(attacker);
            PrintCharacterLog(defender);
        }

        protected void PrintCharacterLog(Character character)
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
            PrintCombatPercentualDamageReduction(character);
            PrintFirstAttackPercentualDamageReduction(character);
            PrintCombatFlatDamageReduction(character);
        }

       
        protected abstract void PrintCombatBonuses(Character character);
        protected abstract void PrintFirstAttackBonuses(Character character);
        protected abstract void PrintFollowupBonuses(Character character);
        protected abstract void PrintCombatPenalties(Character character);
        protected abstract void PrintFirstAttackPenalties(Character character);
        protected abstract void PrintFollowupPenalties(Character character);
        protected abstract void PrintNeutralizedBonuses(Character character);
        protected abstract void PrintNeutralizedPenalties(Character character);
        protected abstract void PrintCombatFlatAttackIncrement(Character character);
        protected abstract void PrintCombatPercentualDamageReduction(Character character);
        protected abstract void PrintFirstAttackPercentualDamageReduction(Character character);
        protected abstract void PrintCombatFlatDamageReduction(Character character);

    }

   
}
