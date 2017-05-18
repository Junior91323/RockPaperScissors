using RockPaperScissors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Models
{
    public class Team : ITeam
    {
        List<IPlayer> _Players;
        string _Name;
        int _Count = 0;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public List<IPlayer> Players
        {
            get
            {
                if (_Players == null)
                    _Players = new List<IPlayer>();

                return _Players;
            }
        }
        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                _Count = value;
            }
        }

        public Team(string name)
        {
            this.Name = name;
            this._Players = new List<IPlayer>();
        }

        public void AddPlayer(IPlayer player)
        {
            try
            {
                if (player == null)
                    throw new NullReferenceException("Player is Null!");

                IPlayer item = _Players.Where(x => x.Name.ToLower().Equals(player.Name)).FirstOrDefault();

                if (item != null)
                    throw new Exception("Member with this name already exists!");

                player.Team = this;

                Players.Add(player);
            }
            catch (Exception ex)
            {
                //LOG ...

                throw new Exception(ex.Message, ex.InnerException);
            }

        }
    }
}
