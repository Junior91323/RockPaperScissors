using RockPaperScissors.Models.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface ICouple
    {
        Guid ID { get;}
        IPlayer FirstPlayer { get; set; }
        IPlayer SecondPlayer { get; set; }
        void StartEvent(object sender, StartRoundEventArgs args);
    }
}
