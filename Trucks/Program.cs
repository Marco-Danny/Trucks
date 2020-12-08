using System;
using System.IO;

namespace Trucks
{
    class Program
    {
        static void Main(string[] args)
        {
            // C:\Users\admin\C# projects\Trucks\Trucks\Trucks.json
            // string path = GetPathFromUser();
            var path = @"C:\Users\admin\C# projects\Trucks\Trucks\Trucks.json";
            var manager = new TruckManager(path);
            TruckManager.Menu();
        }

        private static string GetPathFromUser()
        {
            var wrongPath = "Вы ввели неверный путь";

            while (true)
            {
                Console.WriteLine("Введите путь к файлу: ");
                var path = Console.ReadLine();
                try
                {
                    string content = File.ReadAllText(path);
                    return path;
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine(wrongPath);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine(wrongPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}