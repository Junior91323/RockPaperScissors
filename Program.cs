using RockPaperScissors.Infrastructure;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Start();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR!");
                Console.WriteLine("{0} : {1}", ex.Message, ex.StackTrace);
            }
            finally { Console.ReadKey(); }

        }

        private static void Game_FinishGameHandler(object sender, EventArgs e)
        {
            Game game = sender as Game;
            if (game != null)
            {
                if (game.Team1 != null && game.Team2 != null)
                {

                    foreach (Couple item in game.Couples)
                    {
                        if (item.FirstPlayer.Result > 0 && item.SecondPlayer.Result > 0)
                        {
                            Console.ForegroundColor = item.FirstPlayer.Status == Infrastructure.Options.BET.Win ? ConsoleColor.Green : item.FirstPlayer.Status == item.SecondPlayer.Status? ConsoleColor.Yellow: ConsoleColor.Red;

                            Console.Write("{0} : {1} --- ", item.FirstPlayer.Name, item.FirstPlayer.Result);

                            Console.ForegroundColor = item.SecondPlayer.Status == Infrastructure.Options.BET.Win ? ConsoleColor.Green : item.FirstPlayer.Status == item.SecondPlayer.Status ? ConsoleColor.Yellow : ConsoleColor.Red;
                            Console.Write("{0} : {1} ", item.SecondPlayer.Name, item.SecondPlayer.Result);
                            Console.WriteLine();
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("{0} : {1} ---- {2} : {3}", game.Team1.Name, game.Team1.Count, game.Team2.Name, game.Team2.Count);
                    Console.WriteLine("End GAME!!!");

                    
                }
            }
        }
        private static void Start()
        {
            try
            {
                int playersCount = 0;
                int winsCount = 0;
                Console.WriteLine("Input players count!");
                if (Int32.TryParse(Console.ReadLine(), out playersCount) && playersCount > 0)
                {
                    Console.WriteLine("Input wins count!");
                    if (Int32.TryParse(Console.ReadLine(), out winsCount) && winsCount > 0)
                    {
                        IGame game = new Game(playersCount, winsCount);
                        game.FinishGameHandler += Game_FinishGameHandler;
                        game.Start();
                    }
                    else
                    {
                        Console.WriteLine("Input integer number!");
                        Start();
                    }
                }
                else
                {
                    Console.WriteLine("Input integer number!");
                    Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
