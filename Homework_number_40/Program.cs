using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Homework_number_40
{
    internal class Program
    {
        const string СommandAddUser = "1";
        const string CommandPrint = "2";
        const string CommandBlock = "3";
        const string CommandUnlock = "4";
        const string CommandRemove = "5";
        const string CommandExit = "6";

        static void Main(string[] args)
        {
            Database database = new Database();

            bool isExit = false;
            string userInput;

            while (isExit == false)
            {
                ShowMenu();

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

                    case CommandBlock:
                        SetPlayerBlock(database);
                        break;

                    case CommandUnlock:
                        SetPlayerUnlock(database);
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

        static void ShowMenu()
        {
            Console.WriteLine("Меню\n" +
                           "\nДоступные команды\n\n" +
                           $"1) Добавить игрока ведите {СommandAddUser}\n\n" +
                           $"2) Вывод списка игроков ведите {CommandPrint}\n\n" +
                           $"3) Для того что бы забанить игрока ведите {CommandBlock}\n\n" +
                           $"4) Для того что бы разбанить игрока ведите  {CommandUnlock}\n\n" +
                           $"5) Удалить игрока ведите {CommandRemove}\n\n" +
                           $"6) Для выхода ведите: {CommandExit}" +
                           $"Укажите команду: ");
        }

        private static void AddPlayer(Database database)
        {
            int id = GetNumber("Укажите ID пользователя: ");
            int lavel = GetNumber("Укажите lavel пользователя: ");
            bool isBanned = false;

            Console.Write("Укажите имя пользователя: ");
            string name = Console.ReadLine();

            if (database.TryAdd(new Player(id, name, lavel, isBanned)) == true)
            {
                ShowMessage("Данные пользователя успешно добавлены!");
            }
            else
            {
                ShowMessage("Пользователь с таким ID уже существует в базе!", ConsoleColor.Red);
            }
        }

        private static void RemovePlayer(Database database)
        {
            int id = GetNumber("Укажите ID пользователя для его удаления: ");

            database.Remove(id);
        }

        private static void SetPlayerBlock(Database database)
        {
            int id = GetNumber("Укажите ID пользователя для того что бы его забанить : ");

            database.Block(id);
        }

        private static void SetPlayerUnlock(Database database)
        {
            int id = GetNumber("Укажите ID пользователя для того что бы разбанить пользователя: ");

            database.Unlock(id);
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

        private static void ShowMessage(string text, ConsoleColor consoleColor = ConsoleColor.Green)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ResetColor();
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
        public List<Player> Players { get; private set; }

        public bool TryAdd(Player player)
        {
            if (ContainsId(player.Id) == false)
            {
                Players.Add(player);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Remove(int id)
        {
            Player foundPlayer;

            bool playerFound = TryGetPlayer(out foundPlayer, id);

            if (playerFound == true)
            {
                Players.Remove(foundPlayer);
            }
        }

        public void Block(int id)
        {
            Player foundPlayer;

            bool playerFound = TryGetPlayer(out foundPlayer, id);

            if (playerFound == true)
            {
                foundPlayer.SetBannedStatus(true);
            }
        }

        public void Unlock(int id)
        {
            Player foundPlayer;

            bool playerFound = TryGetPlayer(out foundPlayer, id);

            if (playerFound == true)
            {
                foundPlayer.SetBannedStatus(false);
            }
        }

        private bool TryGetPlayer(out Player player, int id)
        {
            player = null;

            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].Id == id)
                {
                    player = Players[i];

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
    }
}
