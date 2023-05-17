using System;
using System.Collections.Generic;

namespace Homework_number_40
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string СommandAddUser = "1";
            const string CommandPrint = "2";
            const string CommandBlock = "3";
            const string CommandUnlock = "4";
            const string CommandRemove = "5";
            const string CommandExit = "6";

            Database database = new Database();

            bool isExit = false;
            string userInput;

            while (isExit == false)
            {
                Console.WriteLine("Меню\n" +
                        "\nДоступные команды\n\n" +
                        $"1) Добавить игрока ведите {СommandAddUser}\n\n" +
                        $"2) Вывод списка игроков ведите {CommandPrint}\n\n" +
                        $"3) Для того что бы забанить игрока ведите {CommandBlock}\n\n" +
                        $"4) Для того что бы разбанить игрока ведите  {CommandUnlock}\n\n" +
                        $"5) Удалить игрока ведите {CommandRemove}\n\n" +
                        $"6) Для выхода ведите: {CommandExit}\n\n" +
                        $"Укажите команду: ");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case СommandAddUser:
                        database.Add();
                        break;

                    case CommandPrint:
                        database.ShowItems();
                        break;

                    case CommandRemove:
                        database.Remove();
                        break;

                    case CommandBlock:
                        database.Block();
                        break;

                    case CommandUnlock:
                        database.Unlock();
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

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();
        private int _currentId;

        public void Add()
        {
            _currentId++;

            int lavel = GetNumber("Укажите lavel пользователя: ");

            bool isBanned = false;

            ShowMessage("Укажите имя пользователя: ", ConsoleColor.Blue);
            string name = Console.ReadLine();

            _players.Add(new Player(_currentId, name, lavel, isBanned));

            ShowMessage("Данные пользователя успешно добавлены!");
        }

        public void Remove()
        {
            if (TryGetPlayer(out Player player) == true)
            {
                _players.Remove(player);

                ShowMessage("Пользователь успешно удалён из базы данных!");
            }
        }

        public void Block()
        {
            if (TryGetPlayer(out Player player) == true)
            {
                player.Ban();

                ShowMessage("Пользователь успешно заблокирован!");
            }
        }

        public void Unlock()
        {
            if (TryGetPlayer(out Player player) == true)
            {
                player.Unban();

                ShowMessage("Пользователь успешно разблокирован!");
            }
        }

        public void ShowItems()
        {
            if (_players.Count > 0)
            {
                for (int i = 0; i < _players.Count; i++)
                {
                    ShowMessage($"Id: {_players[i].Id} Name: {_players[i].Name} Level: {_players[i].Level} Banned: {_players[i].IsBanned}", ConsoleColor.Yellow);
                }
            }
            else
            {
                ShowMessage("К сожалению ни одного игрока в базе нет ", ConsoleColor.Blue);
            }
        }

        private bool TryGetPlayer(out Player player)
        {
            player = null;

            int id = GetNumber("Укажите ID пользователя для того что бы разбанить пользователя: ");

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Id == id)
                {
                    player = _players[i];

                    return true;
                }
            }

            ShowMessage("К сожалению такого пользователя в базе нет!", ConsoleColor.Red);

            return false;
        }

        private int GetNumber(string text)
        {
            bool isNumber = false;
            string userInput;
            int number = 0;

            while (isNumber == false)
            {
                ShowMessage(text, ConsoleColor.Blue);
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out number))
                {
                    isNumber = true;
                }
                else
                {
                    ShowMessage("Не верный формат вода", ConsoleColor.Red);
                }
            }

            return number;
        }

        private void ShowMessage(string text, ConsoleColor consoleColor = ConsoleColor.Green)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
