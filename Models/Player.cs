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
        RPS _Result;
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

        public BET Status { get; set; }

        public RPS Result
        {
            get
            {
                return _Result;
            }
        }

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
            _Result = (RPS)RandomNumber(1, 4);
            return (_Result);
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
