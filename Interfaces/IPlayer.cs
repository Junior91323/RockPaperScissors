using RockPaperScissors.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IPlayer
    {
        string Name { get;}
        ITeam Team { get; set; }
        Bet Status { get; set; }
        RPS Play();
    }
}
