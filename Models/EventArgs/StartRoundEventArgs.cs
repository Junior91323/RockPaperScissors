using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RockPaperScissors.Models.EventArgs
{
    public class StartRoundEventArgs : System.EventArgs
    {
        public int WinLength { get; set; }
        public string Message { get; set; }
        public List<Task> TaskPool { get; set; }
        public CancellationTokenSource CancelTokenSource { get; set; }
        public CancellationToken Token { get; set; }

    }
}
