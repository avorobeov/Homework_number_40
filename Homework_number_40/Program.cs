using System;
using System.Collections.Generic;

namespace Homework_number_40
{
    internal class Program
    {
        const string СommandAddUser = "1";
        const string CommandPrint = "2";
        const string CommandBan = "3";
        const string CommandUnBan = "4";
        const string CommandRemove = "5";
        const string CommandExit = "6";

        static void Main(string[] args)
        {
            Database database = new Database();

            bool isExit = false;
            string userInput;

            while (isExit == false)
            {
                Console.WriteLine("Меню\n" +
                            "\nДоступные команды\n\n" +
                            $"1) Добавить игрока ведите {СommandAddUser}\n\n" +
                            $"2) Вывод списка игроков ведите {CommandPrint}\n\n" +
                            $"3) Для того что бы забанить игрока ведите {CommandBan}\n\n" +
                            $"4) Для того что бы разбанить игрока ведите  {CommandUnBan}\n\n" +
                            $"5) Удалить игрока ведите {CommandRemove}\n\n" +
                            $"6) Для выхода ведите: {CommandExit}" +
                            $"Укажите команду: ");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case СommandAddUser:
                        AddPlayer(database);
                        break;

                    case CommandPrint:
                        database.ShowPlayers();
                        break;

                    case CommandRemove:
                        RemovePlayer(database);
                        break;

                    case CommandBan:
                        SetPlayerBan(database);
                        break;

                    case CommandUnBan:
                        SetPlayerUnBan(database);
                        break;

                    case CommandExit:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Такой команды нет в наличии!");
                        break;
                }
            }
        }

        private static void AddPlayer(Database database)
        {
            int id = GetNumber("Укажите ID пользователя: ");
            int lavel = GetNumber("Укажите lavel пользователя: ");
            bool isBanned = false;

            Console.Write("Укажите имя пользователя: ");
            string name = Console.ReadLine();

            database.Add(new Player(id, name, lavel, isBanned));
        }

        private static void RemovePlayer(Database database)
        {
            int id = GetNumber("Укажите ID пользователя для его удаления: ");

            database.Remove(id);
        }

        private static void SetPlayerBan(Database database)
        {
            int id = GetNumber("Укажите ID пользователя для того что бы его забанить : ");

            database.Ban(id);
        }

        private static void SetPlayerUnBan(Database database)
        {
            int id = GetNumber("Укажите ID пользователя для того что бы разбанить пользователя: ");

            database.UnBan(id);
        }

        private static int GetNumber(string text)
        {
            bool isNumber = false;
            string userInput;
            int number = 0;

            while (isNumber == false)
            {
                Console.Write(text);
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out number))
                {
                    isNumber = true;
                }
                else
                {
                    Console.WriteLine("Не верный формат вода");
                }
            }

            return number;
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

                ShowMessage("Данные пользователя успешно добавлены!");
            }
            else
            {
                ShowMessage("Пользователь с таким ID уже существует в базе!", ConsoleColor.Red);
            }
        }

        public void Remove(int id)
        {
            Player foundPlayer;

            bool playerFound = TryGetPlayer(out foundPlayer, id);

            if (playerFound == true)
            {
                _players.Remove(foundPlayer);
            }
        }

        public void Ban(int id)
        {
            Player foundPlayer;

            bool playerFound = TryGetPlayer(out foundPlayer, id);

            if (playerFound == true)
            {
                foundPlayer.SetBannedStatus(true);
            }
        }

        public void UnBan(int id)
        {
            Player foundPlayer;

            bool playerFound = TryGetPlayer(out foundPlayer, id);

            if (playerFound == true)
            {
                foundPlayer.SetBannedStatus(false);
            }
        }

        public void ShowPlayers()
        {
            if (_players.Count > 0)
            {
                for (int i = 0; i < _players.Count; i++)
                {
                    Console.WriteLine($"Id: {_players[i].Id} Name: {_players[i].Name} Level: {_players[i].Level} Banned: {_players[i].IsBanned}");
                }
            }
            else
            {
                ShowMessage("К сожалению ни одного игрока в базе нет ", ConsoleColor.Blue);
            }
        }

        private bool TryGetPlayer(out Player player, int id)
        {
            player = null;

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Id == id)
                {
                    player = _players[i];

                    return true;
                }
            }

            return false;
        }

        private bool ContainsId(int id)
        {
            Player foundPlayer;

            return TryGetPlayer(out foundPlayer, id);
        }

        private void ShowMessage(string text, ConsoleColor consoleColor = ConsoleColor.Green)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
