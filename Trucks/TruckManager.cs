using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Trucks
{
    public class TruckManager
    {
        private string _path;
        private static List<Truck> _trucks = new List<Truck>();
        private static bool isCorrectUserChoice(int input, ICollection data) => input > 0 && input <= data.Count;
        private static bool IsHaveTrucksData => _trucks.Count > 0;

        private static List<string> MENU_VARIANTS = new List<string>()
        {
            "Отобразить текущее состояние грузовиков",
            "Показать данные грузовика по id"
        };

        public TruckManager(string path)
        {
            _path = path;
            GetTrucksFromJson(path);
        }

        private void GetTrucksFromJson(string path)
        {
            var dataloader = new DataLoader();
            _trucks = dataloader.Load(path);
        }

        private static int GetUserChoice()
        {
            while (true)
            {
                try
                {
                    var choice = int.Parse(Console.ReadLine());
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
                        truck.State);
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

        private static void ShowMenuVariants()
        {
            for (var i = 0; i < MENU_VARIANTS.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, MENU_VARIANTS[i]);
            }
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