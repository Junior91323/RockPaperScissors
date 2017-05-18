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
            IGame game = new Game(200,999);
            game.Start();

            Console.ReadKey();
        }

    }
}
