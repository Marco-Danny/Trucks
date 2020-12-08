using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static System.Int32;

namespace Trucks
{
    public class TruckManager
    {
        private static string _path;
        private static List<Truck> _trucks = new List<Truck>();
        private static bool isCorrectUserChoice(int input, ICollection data) => input > 0 && input <= data.Count;
        private static bool IsHaveTrucksData => _trucks.Count > 0;

        private static List<string> MENU_VARIANTS = new List<string>()
        {
            "Отобразить текущее состояние грузовиков",
            "Показать данные грузовика по id",
            "Изменить водителя",
            "Обновить состояние грузовика",
            "Выгрузить данные"
        };

        public TruckManager(string path)
        {
            _path = path;
            GetTrucksFromJson(path);
        }

        private void GetTrucksFromJson(string path)
        {
            var dataloader = new DataLoader(_path);
            _trucks = dataloader.Load(path);
        }

        private static int GetUserChoice()
        {
            while (true)
            {
                try
                {
                    var choice = Parse(Console.ReadLine());
                    return choice;
                }
                catch (Exception)
                {
                    Console.WriteLine("Нужно ввести число");
                }
            }
        }

        private static void ShowAllTrucks()
        {
            if (_trucks.Count != 0)
            {
                Console.WriteLine("№ 	| Грузовик\t\t| Водитель \t| Состояние ");
                foreach (var truck in _trucks)
                {
                    Console.WriteLine(
                        "{0}\t| {1}\t\t| {2}\t\t| {3} ",
                        truck.Id,
                        truck.Name,
                        truck.Driver,
                        truck.State.ToString());
                }
            }
            else
            {
                Console.WriteLine("Данных о грузовиках нет!!!");
            }
        }

        private static Truck GetTruck()
        {
            while (true)
            {
                Console.WriteLine("Введите ID грузовика: ");
                ShowAllTrucks();
                int userChoice = GetUserChoice();
                if (isCorrectUserChoice(userChoice, _trucks))
                {
                    return _trucks[userChoice - 1];
                }
                else
                {
                    Console.WriteLine("Такого грузовика нет");
                    Console.WriteLine();
                }
            }
        }

        private static void ShowTruckById()
        {
            Console.WriteLine();
            if (IsHaveTrucksData)
            {
                var truck = GetTruck();
                Console.WriteLine(truck.ToString());
            }
            else
            {
                Console.WriteLine("Данных о грузовиках нет!!!");
            }
        }

        private static void ChangeTruckDriver()
        {
            if (IsHaveTrucksData)
            {
                var truck = GetTruck();
                truck.ChangeDriver();
            }
            else
            {
                Console.WriteLine("Данных о грузовиках нет!");
            }
        }

        private static string GetIdAndStateFromUser()
        {
            while (true)
            {
                Console.WriteLine("Введите через пробел ID и состояние грузовика");
                var userInput = Console.ReadLine();
                if (userInput != "" && userInput != " ")
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("Вы ничего не ввели!!!");
                }
            }
        }

        private static string[] ParseUserStringIdState()
        {
            while (true)
            {
                var userString = GetIdAndStateFromUser();
                string[] data = userString.Split(" ");
                var inputId = data[0];
                var inputState = data[1];
                if (inputState != "run" || inputState != "repair")
                {
                    try
                    {
                        Parse(inputId);
                        return data;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("ид должно быть числвым");
                    }
                }
                else
                {
                    Console.WriteLine("Состояние должно быть \"run\" или \"repair\" ");
                }
            }
        }

        private static void ChangeTruckState()
        {
            if (IsHaveTrucksData)
            {
                while (true)
                {
                    string[] inputIdAndState = ParseUserStringIdState();
                    var inputId = inputIdAndState[0];
                    var inputState = inputIdAndState[1];
                    var Id = Parse(inputId);
                    Truck truck = null;
                    foreach (var tr in _trucks)
                    {
                        if (tr.Id == Id)
                        {
                            truck = tr;
                        }
                    }
                    
                    if (isCorrectUserChoice(Id, _trucks))
                    {
                        switch (inputState)
                        {
                            case "run":
                                truck._status.StartRun();
                                break;
                            case "repair":
                                truck._status.StartRepair();
                                break;
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Нет такого грузовика!!!");
                    }
                }
            }
            else
            {
                Console.WriteLine("Данных о грузовиках нет!");
            }
        }

        private static void ShowMenuVariants()
        {
            for (var i = 0; i < MENU_VARIANTS.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, MENU_VARIANTS[i]);
            }
        }

        private static void LoadData()
        {
            var dataloader = new DataLoader(_path);
            dataloader.Save(_trucks);
        }

        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите вариант: ");
                ShowMenuVariants();
                int menuNumber = GetUserChoice();
                if (isCorrectUserChoice(menuNumber, MENU_VARIANTS))
                {
                    switch (menuNumber)
                    {
                        case 1:
                            ShowAllTrucks();
                            break;
                        case 2:
                            ShowTruckById();
                            break;
                        case 3:
                            ChangeTruckDriver();
                            break;
                        case 4:
                            ChangeTruckState();
                            break;
                        case 5:
                            LoadData();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Такого варианта нет!!!");
                }
            }
        }
    }
}