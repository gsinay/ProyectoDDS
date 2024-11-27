using Fire_Emblem_GUI;
using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Player;

namespace Fire_Emblem.Views.CombatLoggers;

public class WindowView : ILogger

{
        private FireEmblemWindow _window = new ();
        
        public void Start(Action startProgram) 
            => _window.Start(startProgram);

        public void AskForFile() {}
        
        public int GetUserFileInput()
        {
            return 1;}

     
        public void PrintNoFileFound(){}
        public void PrintFileNumberAndName(int number, string fileName){}

        public void PrintGameNotValid()
        {
            _window.ShowInvalidTeamMessage();
        }
        
        public string[] GetTeamsFromUser()
        {
            string team1Data = _window.GetTeam1(); 
            string team2Data = _window.GetTeam2();
            string returnData = "Player 1 Team\n" + team1Data + "\nPlayer 2 Team\n" + team2Data;
            return returnData.Split("\n");
        }

        public void UpdateTeams(Player attacker, Player defender)
        {
           IUnit[] Player1IUnits = attacker.GetMyIUnits();
           IUnit[] Player2IUnits = defender.GetMyIUnits();
           _window.UpdateTeams(Player1IUnits, Player2IUnits);
        }
        
        public int GetUserCharacterInput(Player player)
        {
            if(player.PlayerNumber == 1)
                return _window.SelectUnitTeam1();
            return _window.SelectUnitTeam2();
        }
        public void AnnounceTurn(int turn, string characterName, int playerNumber){}
        public void AnnounceOption(int playerNumber){}

        public void ListCharacters(Player player)
        { }
        public  void PrintAdvantage(Character attacker, Character defender, double wtb){}
        public  void DisplayAttackResult(Character attacker, Character defender, int damage){}
        public  void AnnounceNoFollowUps(Character attacker, Character defender){}
        public  void AnnounceResults(Character attacker, Character defender){}
        public void AnnounceWinner(int playerNumber){}

        public void PrintPreCombatLog(Character attacker, Character defender)
        {
            
        }
        
        public void PrintPostCombatLog(Character attacker, Character defender){}
        
        public void DisplayHealingResult(Character character, int healingAmount){}
    
     
        
}