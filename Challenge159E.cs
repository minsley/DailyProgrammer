using System;
using System.Collections.Generic;

namespace DailyProg
{
    class Challenge159E
    {
        public static void MainMethod()
        {
            var stats = new Stats();
            var rand = new Random();

            var stillPlaying = true;
            var gameLimit = 0;
            var nPlayers = 0;

            Console.WriteLine("Now playing Rock-Paper-Scissors-Lizard-Spock (RPSLK)!");
            Console.WriteLine("How many games would you like to play (0 = unlimited)?");
            int.TryParse(Console.ReadLine(), out gameLimit);
            Console.WriteLine("How many people will be playing? (0 - 2)");
            int.TryParse(Console.ReadLine(), out nPlayers);

            while (stillPlaying)
            {
                // Get player moves
                var playerMoves = new string[2];
                for (var i=0; i < 2; i++)
                {
                    if (i < nPlayers)
                    {
                        Console.Write("Player {0}, choose a move: ", i+1);
                        playerMoves[i] = Console.ReadLine().ToLower();
                    }
                    else
                    {
                        playerMoves[i] = Game.GetComputerMove(rand);
                        Console.WriteLine("Player {0} picked: {1}", i+1, playerMoves[i]);
                    }
                }

                // Play the round
                var winner = Game.Toss(playerMoves[0], playerMoves[1]);

                // Show results
                Game.AnnounceWinner(winner, playerMoves);

                // Record results
                stats.RecordGame(winner);

                // Continue?
                if (--gameLimit == 0)
                {
                    stillPlaying = false;
                    Console.WriteLine("Game limit reached! Thanks for playing");
                }
                else if(nPlayers != 0)
                { 
                    Console.Write("Play again? (y/n): ");
                    if(Console.ReadLine() == "n") stillPlaying = false;
                }
            }

            stats.AnnounceStats();
        }

        public static class Game
        {
            internal static readonly string[] Moves =
            {
                "rock",
                "paper",
                "scissors",
                "lizard",
                "spock"
            };

            internal static readonly Dictionary<string, string> Plays = new Dictionary<string, string>
            {
                {"scissorspaper","cuts"},
                {"paperrock","covers"},
                {"rocklizard", "crushes"},
                {"lizardspock", "poisoins"},
                {"spockscissors", "smashes"},
                {"scissorslizard", "decapitates"},
                {"lizardpaper", "eats"},
                {"paperspock", "disproves"},
                {"spockrock", "vaporizes"},
                {"rockscissors", "crushes"}
            };

            internal static string GetComputerMove(Random generator)
            {
                return Moves[generator.Next(0,4)];
            }

            internal static int Toss(string p1Hand, string p2Hand)
            {
                if (p1Hand == p2Hand) return 0;
                if (Plays.ContainsKey(p1Hand + p2Hand)) return 1;
                if (Plays.ContainsKey(p2Hand + p1Hand)) return 2;
                return -1;
            }

            internal static void AnnounceWinner(int winner, string[] playerMoves)
            {
                switch (winner)
                {
                    case 0:
                        Console.WriteLine("Tie game.");
                        break;
                    case 1:
                        Console.WriteLine(playerMoves[0] + " "
                            + Plays[playerMoves[0] + playerMoves[1]] + " "
                            + playerMoves[1]);
                        Console.WriteLine("Player {0} wins!", winner);
                        break;
                    case 2:
                        Console.WriteLine(playerMoves[1] + " "
                            + Plays[playerMoves[1] + playerMoves[0]] + " "
                            + playerMoves[0]);
                        Console.WriteLine("Player {0} wins!", winner);
                        break;
                }
            }
        }

        public struct Stats
        {
            internal int GamesPlayed;
            internal int P1Wins;
            internal int P2Wins;
            internal int Ties;

            public void RecordGame(int winner)
            {
                GamesPlayed++;
                switch (winner)
                {
                    case 0:
                        Ties++;
                        break;
                    case 1:
                        P1Wins++;
                        break;
                    default:
                        P2Wins++;
                        break;
                }
            }

            public void AnnounceStats()
            {
                Console.WriteLine("Games played: {0}\n" +
                                  "Player1 wins: {1} ({2}%)\n" +
                                  "Player2 wins: {3} ({4}%)\n" +
                                  "Ties: {5} ({6}%)", 
                                  GamesPlayed,
                                  P1Wins, P1Wins * 100 / GamesPlayed,
                                  P2Wins, P2Wins * 100 / GamesPlayed,
                                  Ties, Ties * 100 / GamesPlayed);
            }
        }
    }
}
