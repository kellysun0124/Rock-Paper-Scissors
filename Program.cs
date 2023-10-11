using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalProject
{
    class Program
    {
        static string MenuChoice()
        {
            //menu
            List<Players> playersList = DataLoader.playerList();
            bool repeat = true;
            string username = "";
            Console.WriteLine("Welcome to Rock, Paper, Scissors!\n");
            while(repeat){
                Console.WriteLine("    1. Start New Game\n    2. Load Game\n    3. Quit\n");
                Console.Write("Enter choice: ");
                string userInput = Console.ReadLine();
                repeat = false;

                if (userInput == "1"){   
                    Console.Write("What is your name? ");
                    username = Console.ReadLine();
                    Console.WriteLine($"Hello {username}. Let's play!");
                    using(StreamWriter addline = File.AppendText("player_log.csv"))
                    {
                        addline.Write($"\n{username},0,0,0");
                        addline.Close();
                    }
                    break;
                } if (userInput == "2"){
                    Console.Write("What is your name? ");
                    username = Console.ReadLine();
                    var playerSearch = from player in playersList where player.name == username select player;
                    int existCounter = 0;
                    foreach (var player in playerSearch){
                        existCounter ++;
                    }
                    if (existCounter == 0 ){
                        Console.WriteLine($"{username}, your game could not be found.");
                        repeat = true;
                        continue;
                    } else {
                        Console.WriteLine($"Welcome back {username}. Lets play!");
                        break;
                    }
                } if (userInput == "3"){
                    Environment.Exit(0);
                }else {
                    Console.WriteLine("Unaccepted input.");
                    Console.WriteLine("Please input one of the menu options.");
                    repeat = true;
                }
            }
            return username;
        }
        static string GamePlay(string user)
        {
            var username = user;
            List<Players> playersList = DataLoader.playerList();
            //gameplay choices menu
            var movesList = new List<string>{"rock", "paper", "scissor"};
            //update data in player_log.csv
            var selectPlayer = from player in playersList where player.name == username select player;
            
            string result = "";
            
            //playagain loop
            bool playAgain = true;
            while (playAgain){
                //used in play again
                string userMove = "";
                bool userError = true;

                // PC move
                Random random = new Random();
                int randomMove = random.Next(3);

                while (userError){
                    Console.WriteLine("    1. Rock\n    2. Paper\n    3. Scissors\n");
                    Console.Write("What will it be? ");
                    //user move
                    string userChoice = Console.ReadLine();
                    //deciding result
                    if (userChoice == "1"){
                        userMove = "rock";
                        if (randomMove == 0){
                            result = "tie";
                        } if (randomMove == 1){
                            result = "lose";
                        } if (randomMove == 2){
                            result = "win";
                        }
                        userError = false;
                        break;
                    } if (userChoice == "2"){
                        userMove = "paper";
                        if (randomMove == 0){
                            result = "win";
                        } if (randomMove == 1){
                            result = "tie";
                        } if (randomMove == 2){
                            result = "lose";
                        }
                        userError = false;
                        break;
                    } if (userChoice == "3"){
                        userMove = "scissor";
                        if (randomMove == 0){
                            result = "lose";
                        } if (randomMove == 1){
                            result = "win";
                        } if (randomMove == 2){
                            result = "tie";
                        }
                        userError = false;
                        break;
                    } else {
                        Console.WriteLine("Unaccepted input");
                        Console.WriteLine("Please input 1 for rock, 2 for paper or 3 for scissors.");
                        userError = true;
                    }
                }
                Console.WriteLine($"You chose {userMove}. The computer chose {movesList[randomMove]}. You {result}!");
                /* i should move???
                if (result == "win"){
                    wins += 1;
                } if (result == "lose"){
                    losses += 1;
                } if (result == "tie"){
                    ties += 1;
                }
                
                foreach (var player in selectPlayer){
                    player.setWins(wins);
                    player.setLosses(losses);
                    player.setTies(ties);
                }
                */

                break;
            }
            return result;
        }
        static void Menu()
        {
            var username = MenuChoice();
            List<Players> playersList = DataLoader.playerList();
            var selectPlayer = from player in playersList where player.name == username select player;
            int wins = 0;
            int losses = 0;
            int ties = 0;
            int roundCounter = 0;
            foreach (var playerData in selectPlayer){
                wins = playerData.wins;
                losses = playerData.losses;
                ties = playerData.ties;
                roundCounter = playerData.wins + playerData.losses + playerData.ties +1;
            }
            
            Console.WriteLine($"\nRound: {roundCounter}");
            string result = GamePlay(username);

            if (result == "win"){
                wins += 1;
            } if (result == "lose"){
                losses += 1;
            } if (result == "tie"){
                ties += 1;
            }
            foreach (var player in selectPlayer){
            player.setWins(wins);
            player.setLosses(losses);
            player.setTies(ties);
            }
            

            //PLAY AGAIN
            bool playAgain = true;
            while (playAgain){
                foreach (var playerData in selectPlayer){
                wins = playerData.wins;
                losses = playerData.losses;
                ties = playerData.ties;  
                }
                Console.WriteLine("What would you like to do?\n");
                Console.WriteLine("    1. Play again\n    2. View Player Statistics\n    3. View Leaderboard\n    4. Quit\n");
                Console.Write("Enter choice: ");
                var playerChoice = Console.ReadLine();   
                if (playerChoice == "1"){
                    roundCounter ++;
                    Console.WriteLine($"\nRound {roundCounter}");
                    result = GamePlay(username);
                    if (result == "win"){
                        wins += 1;
                    } if (result == "lose"){
                        losses += 1;
                    } if (result == "tie"){
                        ties += 1;
                    }
                    foreach (var player in selectPlayer){
                    player.setWins(wins);
                    player.setLosses(losses);
                    player.setTies(ties);
                    }
                    try{
                        using (var write = new StreamWriter("player_log.csv")){
                            foreach (var player in playersList){
                                write.WriteLine($"{player.name},{player.wins},{player.losses},{player.ties}");
                            }
                        }
                        Console.WriteLine($"{username}, your game has been saved.");
                    }catch (Exception){
                        Console.WriteLine($"Sorry {username}, the game could not be saved.");
                    }
                } if (playerChoice == "2"){
                    Console.WriteLine($"{username}, here are your game play statistics: ");
                    foreach (var player in selectPlayer){
                        Console.WriteLine($"Wins: {player.wins}");
                        Console.WriteLine($"Losses: {player.losses}");
                        Console.WriteLine($"Ties: {player.ties}");
                        double win = Convert.ToDouble(player.wins);
                        double loss = Convert.ToDouble(player.losses);
                        double ratio = 0;
                        if (loss == 0){
                            Console.WriteLine("Ratio is indeterminate because you have not loss yet.");
                        }else {
                            ratio = win/loss;
                            Console.WriteLine($"\nWin/Loss ratio: {Math.Round(ratio, 2)}\n");
                        }
                    }
                } if (playerChoice == "3"){
                    //top 10 players with wins
                    var allPlayers = from players in playersList where players.wins >= 0 orderby players.wins descending select players;
                    int playerCounter = 0;
                    Console.WriteLine("\nGlobal Game Statistics");
                    Console.WriteLine("----------------------");
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Top 10 winning players");
                    Console.WriteLine("----------------------");

                    foreach (var players in allPlayers){
                        if (playerCounter <= 9){
                            Console.WriteLine($"{players.name}: {players.wins} wins");
                            playerCounter ++;
                        }
                    }

                    //top 5 players who played the most games with games played
                    Console.WriteLine("\n----------------------");
                    Console.WriteLine("Most Games Played");
                    Console.WriteLine("----------------------");
                    int gamesPlayed = 0;
                    var gamesPlayedDict = new Dictionary<string, int>();
                    //put into dictionary<name, totalgames>
                    foreach (var player in allPlayers){
                        gamesPlayed = player.wins+player.losses+player.ties;
                        try{
                            gamesPlayedDict.Add(player.name, gamesPlayed);
                        }
                        catch{
                            Console.WriteLine($"Duplicate save encountered for {player.name}");
                        }
                    }
                    var gamesPlayedSorted = from player in gamesPlayedDict orderby player.Value descending select player;
                    //sort by total games
                    int totalGamesCounter = 0;
                    foreach(KeyValuePair<string, int> player in gamesPlayedSorted){
                        if (totalGamesCounter <= 4){
                            Console.WriteLine($"{player.Key}: {player.Value} games played");
                            totalGamesCounter ++;
                        }
                    }

                    //overall win/loss ratio
                    double totalWins = 0;
                    double totalLosses = 0;
                    foreach (var player in allPlayers){
                        totalWins += Convert.ToDouble(player.wins);
                        totalLosses += Convert.ToDouble(player.losses);
                    }
                    double totalRatio = totalWins / totalLosses;
                    Console.WriteLine("\n----------------------");
                    Console.WriteLine($"Overall Win/Loss Ratio: {Math.Round(totalRatio, 2)}");
                    Console.WriteLine("----------------------");

                    //total games played by everyone
                    int totalGames = 0;
                    int playerTotalGames = 0;
                    foreach (var player in allPlayers){
                        playerTotalGames = player.wins + player.losses + player.ties;
                        totalGames += playerTotalGames;
                    }
                    Console.WriteLine("\n----------------------");
                    Console.WriteLine($"Total Games Played: {totalGames}");
                    Console.WriteLine("----------------------");

                } if (playerChoice == "4"){
                    playAgain = false;
                    //updatefile
                    try{
                        using (var write = new StreamWriter("player_log.csv")){
                            foreach (var player in playersList){
                                write.WriteLine($"{player.name},{player.wins},{player.losses},{player.ties}");
                            }
                        }
                        Console.WriteLine($"{username}, your game has been saved.");
                    }catch (Exception){
                        Console.WriteLine($"Sorry {username}, the game could not be saved.");
                    }
                    Environment.Exit(0);
                }
            }
        }

        static void Main(string[] args)
        {
            Menu();
        }
    }
}
