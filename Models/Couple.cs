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
            try
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
                                SecondPlayer.Status = CheckWin((Int32)secondPlayerRes, (Int32)firstPlayerRes);

                                switch (FirstPlayer.Status)
                                {
                                    case BET.Win: FirstPlayer.Team.Count++; break;
                                    case BET.Lose: SecondPlayer.Team.Count++; break;
                                }

                                //Console.WriteLine("{0} : {1} --- {2} : {3}", FirstPlayer.Name, firstPlayerRes, SecondPlayer.Name, secondPlayerRes);
                            }

                            if (args.CancelTokenSource != null && (FirstPlayer.Team.Count >= args.WinLength || SecondPlayer.Team.Count >= args.WinLength))
                                args.CancelTokenSource.Cancel();
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error playing in couple", ex.InnerException);
            }
        }

        BET CheckWin(int item, int value)
        {
            switch (item)
            {
                case (Int32)RPS.Rock:
                    if (value == (Int32)RPS.Rock)
                        return BET.Draw;

                    if (value == (Int32)RPS.Papper)
                        return BET.Lose;

                    if (value == (Int32)RPS.Scissors)
                        return BET.Win;

                    break;
                case (Int32)RPS.Papper:
                    if (value == (Int32)RPS.Rock)
                        return BET.Win;

                    if (value == (Int32)RPS.Papper)
                        return BET.Draw;

                    if (value == (Int32)RPS.Scissors)
                        return BET.Lose;
                    break;
                case (Int32)RPS.Scissors:
                    if (value == (Int32)RPS.Rock)
                        return BET.Lose;

                    if (value == (Int32)RPS.Papper)
                        return BET.Win;

                    if (value == (Int32)RPS.Scissors)
                        return BET.Draw;
                    break;
            }
            return 0;
        }
    }
}
