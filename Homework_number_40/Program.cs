using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Homework_number_40
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();

            int id = 1;
            string name = "inna";
            int lavel = 1;
            bool isBanned = false;

            database.Add(new Player(id, name, lavel, isBanned));
            database.Add(new Player(id, name, lavel, isBanned));
            database.Ban(id);
            database.UnBan(id);
            database.Remove(id);
        }
    }

    class Player
    {
        public Player(int id, string name, int lavel, bool isBanned)
        {
            Id = id;
            Name = name;
            Level = lavel;
            IsBanned = isBanned;
        }

        public int Id { get; private set; }
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }
        public string Name { get; private set; }

        public void SetBannedStatus(bool isBanned)
        {
            IsBanned = isBanned;
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();

        public void Add(Player player)
        {
            if (ContainsId(player.Id) == false)
            {
                _players.Add(player);
            }
        }

        public void Remove(int id)
        {
            Player Player = GetPlayer(id);

            if (Player != null)
            {
                _players.Remove(Player);
            }
        }

        public void Ban(int id)
        {
            Player Player = GetPlayer(id);

            if (Player != null)
            {
                Player.SetBannedStatus(true);
            }
        }

        public void UnBan(int id)
        {
            Player Player = GetPlayer(id);

            if (Player != null)
            {
                Player.SetBannedStatus(false);
            }
        }

        private Player GetPlayer(int id)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Id == id)
                {
                    return _players[i];
                }
            }

            return null;
        }

        private bool ContainsId(int id)
        {
            Player Player = GetPlayer(id);

            if (Player == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
