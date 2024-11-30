using Fire_Emblem.Controllers.Combat;
using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Player;

namespace Fire_Emblem.Views.CombatLoggers
{
    public interface ILogger
    {
        void AskForFile();
        
        int GetUserFileInput();

        void PrintNoFileFound();
        void PrintFileNumberAndName(int number, string fileName);
        void PrintGameNotValid();
        string[] GetTeamsFromUser();
        
        void UpdateTeams(Player attacker, Player defender);

        int GetUserCharacterInput(Player player);

        void AnnounceTurn(int turn, string characterName, int playerNumber);
        void AnnounceOption(int playerNumber);
        void ListCharacters(Player player);
        void PrintAdvantage(Character attacker, Character defender, double wtb);
        
        void PrintPreCombatLog(Character attacker, Character defender);

        void DisplayAttackResult(CombatContext context);
        void AnnounceNoFollowUps(Character attacker, Character defender);
        void AnnounceResults(Character attacker, Character defender);
        void AnnounceWinner(Player player);
        void PrintPostCombatLog(Character attacker, Character defender);
        void DisplayHealingResult(Character character, int healingAmount);
    }
}