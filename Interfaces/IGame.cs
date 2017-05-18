using RockPaperScissors.Infrastructure;
using RockPaperScissors.Models.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{

    public interface IGame
    {
        int TeamLength { get; }
        int WinLength { get; }
        ITeam Team1 { get; }
        ITeam Team2 { get; }
        List<ICouple> Couples { get; }
        void Start();

        event StartRoundDelegate StartRoundHandler;
    }
}
