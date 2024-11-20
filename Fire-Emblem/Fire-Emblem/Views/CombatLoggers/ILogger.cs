using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Player;

namespace Fire_Emblem.Views.CombatLoggers
{
    public interface ILogger
    {
        string GetUserInput();
        void AnnounceTurn(int turn, string characterName, int playerNumber);
        void AnnounceOption(int playerNumber);
        void ListCharacters(Player player);
        void PrintAdvantage(Character attacker, Character defender, double wtb);
        void DisplayAttackResult(Character attacker, Character defender, int damage);
        void AnnounceNoFollowUps(Character attacker, Character defender);
        void AnnounceResults(Character attacker, Character defender);
        void AnnounceWinner(int playerNumber);
        void PrintPreCombatLog(Character attacker, Character defender);
    }
}