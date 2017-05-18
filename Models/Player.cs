using RockPaperScissors.Infrastructure.Options;
using RockPaperScissors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RockPaperScissors.Models
{
    public class Player : IPlayer
    {
        string _Name;
        ITeam _Team;
        static readonly Random _Random;
        static readonly object _SyncLock;

        public string Name
        {
            get { return _Name; }
        }
        public ITeam Team
        {
            get { return _Team; }
            set { _Team = value; }
        }

        public Bet Status { get; set; }
        

        static Player()
        {
            _Random = new Random();
            _SyncLock = new object();
        }

        public Player(string name)
        {
            this._Name = name;
        }

        public RPS Play()
        {
            int res = 0;
            res = RandomNumber(1, 4);
            return ((RPS)res);
        }

        private static int RandomNumber(int min, int max)
        {
            lock (_SyncLock)
            {
                return _Random.Next(min, max);
            }
        }
    }
}
