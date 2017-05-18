using RockPaperScissors.Infrastructure.Options;
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
    public class Couple : ICouple
    {
        Guid _ID;
        static readonly object _SyncLock;
        public Guid ID { get { return _ID; } }

        public IPlayer FirstPlayer { get; set; }
        public IPlayer SecondPlayer { get; set; }

        static Couple()
        {
            _SyncLock = new object();
        }
        public Couple(IPlayer first, IPlayer second)
        {
            _ID = Guid.NewGuid();
            this.FirstPlayer = first;
            this.SecondPlayer = second;
        }

        public void StartEvent(object sender, StartRoundEventArgs args)
        {
            if (args != null && args.TaskPool != null)
            {
                args.TaskPool.Add(Task.Factory.StartNew(() =>
                {
                    lock (_SyncLock)
                    {
                        if (args.Token != null && !args.Token.IsCancellationRequested)
                        {
                            RPS firstPlayerRes = FirstPlayer.Play();
                            RPS secondPlayerRes = SecondPlayer.Play();

                            FirstPlayer.Status = CheckWin((Int32)firstPlayerRes, (Int32)secondPlayerRes);

                            switch (FirstPlayer.Status)
                            {
                                case Bet.Win: FirstPlayer.Team.Count++; break;
                                case Bet.Lose: SecondPlayer.Team.Count++; break;
                            }
                            
                            Console.WriteLine("{0} : {1} --- {2} : {3}", FirstPlayer.Name, firstPlayerRes, SecondPlayer.Name, secondPlayerRes);
                        }

                        if (args.CancelTokenSource != null && (FirstPlayer.Team.Count >= args.WinLength || SecondPlayer.Team.Count >= args.WinLength))
                            args.CancelTokenSource.Cancel();
                    }
                }));
            }
        }

        Bet CheckWin(int item, int value)
        {
            switch (item)
            {
                case (Int32)RPS.Rock:
                    if (value == (Int32)RPS.Rock)
                        return Bet.Draw;

                    if (value == (Int32)RPS.Papper)
                        return Bet.Lose;

                    if (value == (Int32)RPS.Scissors)
                        return Bet.Win;

                    break;
                case (Int32)RPS.Papper:
                    if (value == (Int32)RPS.Rock)
                        return Bet.Win;

                    if (value == (Int32)RPS.Papper)
                        return Bet.Draw;

                    if (value == (Int32)RPS.Scissors)
                        return Bet.Lose;
                    break;
                case (Int32)RPS.Scissors:
                    if (value == (Int32)RPS.Rock)
                        return Bet.Lose;

                    if (value == (Int32)RPS.Papper)
                        return Bet.Win;

                    if (value == (Int32)RPS.Scissors)
                        return Bet.Draw;
                    break;
            }
            return 0;
        }
    }
}
