using RockPaperScissors.Models.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Infrastructure
{
    public delegate void StartRoundDelegate(object sender, StartRoundEventArgs args);
}
