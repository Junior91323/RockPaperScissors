using RockPaperScissors.Infrastructure;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Models.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RockPaperScissors.Models
{
    public class Game : IGame
    {
        ITeam _Team1;
        ITeam _Team2;
        List<ICouple> _Couples;
        int _TeamLength;
        int _WinLength;

        List<Task> TaskPool { get; set; }
        CancellationTokenSource CancelTokenSource { get; set; }
        CancellationToken Token { get; set; }


        public event StartRoundHandler StartGameHandler;
        public event EventHandler FinishGameHandler;

        public List<ICouple> Couples
        {
            get
            {
                if (_Couples == null)
                    _Couples = new List<ICouple>();

                return _Couples;
            }
        }
        public ITeam Team1
        {
            get
            {
                if (_Team1 == null)
                    _Team1 = new Team("Team1");

                return _Team1;
            }
        }
        public ITeam Team2
        {
            get
            {
                if (_Team2 == null)
                    _Team2 = new Team("Team2");

                return _Team2;
            }
        }

        public int TeamLength
        {
            get
            {
                return _TeamLength;
            }
        }

        public int WinLength
        {
            get
            {
                return _WinLength;
            }
        }

        public Game(int teamLength, int winLength)
        {
            _Team1 = new Team("Team1");
            _Team2 = new Team("Team2");

            FillTeam(Team1, teamLength);
            FillTeam(Team2, teamLength);

            Init(teamLength, winLength);
        }
        public Game(ITeam team1, ITeam team2, int winLength)
        {
            if (team1 == null || team2 == null)
                throw new NullReferenceException("");

            if (team1.Count != team2.Count)
                throw new Exception("");

            this._Team1 = team1;
            this._Team2 = team2;

            Init(team1.Count, winLength);
        }
        public void Start()
        {
            try
            {
                StartRoundEventArgs args = new StartRoundEventArgs();
                args.TaskPool = TaskPool;
                args.Token = Token;
                args.CancelTokenSource = CancelTokenSource;
                args.WinLength = WinLength;

                StartGameHandler?.Invoke(this, args);

                Task.WaitAll(TaskPool.ToArray(), Token);
            }
            catch (OperationCanceledException ex)
            {
                //throw new Exception("Stop game");
            }
            catch (Exception ex)
            {
                throw new Exception("Start game exception", ex.InnerException);
            }
            finally
            {
                if (FinishGameHandler != null)
                {
                    FinishGameHandler.Invoke(this, new System.EventArgs());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0} : {1} ---- {2} : {3}", Team1.Name, Team1.Count, Team2.Name, Team2.Count);
                    Console.WriteLine("End GAME!!!");
                }
            }
        }
        void Init(int players, int wins)
        {
            _TeamLength = players;
            _WinLength = wins;

            _Couples = new List<ICouple>();

            for (int i = 0; i < _TeamLength; i++)
            {
                ICouple couple = new Couple(Team1.Players[i], Team2.Players[i]);
                StartGameHandler += couple.StartEvent;
                Couples.Add(couple);
            }

            TaskPool = new List<Task>();
            CancelTokenSource = new CancellationTokenSource();
            Token = CancelTokenSource.Token;
        }
        void FillTeam(ITeam team, int count)
        {
            for (int i = 0; i < count; i++)
            {
                IPlayer item = new Player(String.Format("Player{0}_{1}", i, team.Name));
                team.AddPlayer(item);
            }
        }


    }
}
