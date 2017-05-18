using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface ITeam
    {
        int Count { get; set; }
        string Name { get; set; }
        List<IPlayer> Players { get;}
        void AddPlayer(IPlayer player);
    }
}
