using Fire_Emblem_GUI;
using Fire_Emblem.Controllers.Combat;
using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Player;

namespace Fire_Emblem.Views.CombatLoggers;

public class WindowLogger : ILogger

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
            IUnit[] player1IUnits = (attacker.PlayerNumber == 1)
                ? attacker.GetMyIUnits()
                : defender.GetMyIUnits();

            IUnit[] player2IUnits = (attacker.PlayerNumber == 1)
                ? defender.GetMyIUnits()
                : attacker.GetMyIUnits();

            _window.UpdateTeams(player1IUnits, player2IUnits);
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

        public void DisplayAttackResult(CombatContext context)
        {
            IUnit team1Unit = (context.Attacker.PlayerNumber == 1)
                ? context.Attacker.GetMyUnit(context.AttackCharIndex)
                : context.Defender.GetMyUnit(context.DefendingCharIndex);

            IUnit team2Unit = (context.Attacker.PlayerNumber == 1)
                ? context.Defender.GetMyUnit(context.DefendingCharIndex)
                : context.Attacker.GetMyUnit(context.AttackCharIndex);
            _window.UpdateUnitsStatsDuringBattle(team1Unit, team2Unit);

            if (context.Attacker.PlayerNumber == 1)
                _window.ShowAttackFromTeam1(team1Unit, team2Unit);
            else
                _window.ShowAttackFromTeam2(team1Unit, team2Unit);
        }

        public  void AnnounceNoFollowUps(Character attacker, Character defender){}
        public  void AnnounceResults(Character attacker, Character defender){}

        public void AnnounceWinner(Player player)
        {
            IUnit[] playerUnits = player.GetMyIUnits();

            if (player.PlayerNumber == 1)
                _window.CongratulateTeam1(playerUnits);

            else
                _window.CongratulateTeam2(playerUnits);
        }

        public void PrintPreCombatLog(Character attacker, Character defender)
        {
            
        }
        
        public void PrintPostCombatLog(Character attacker, Character defender){}
        
        public void DisplayHealingResult(Character character, int healingAmount){}
    
     
        
}