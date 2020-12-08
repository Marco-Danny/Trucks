using System;

namespace Trucks
{
    public class InBaseState : IState
    {
        private Truck _truck;
        private static string Name = "base";
        
        public InBaseState(Truck truck) => _truck = truck;

        private string GetDriver()
        {
            while (true)
            {
                Console.WriteLine("Введите имя водителя:");
                var inputName = Console.ReadLine();
                if (inputName != "" && inputName != " ")
                {
                    return inputName;
                }
                else
                {
                    Console.WriteLine("Вы ничего не ввелии!!!");
                }
            }
        }
        
        public void ChangeDriver()
        {
            var oldDriver = _truck.Driver;
            string newDriver = GetDriver();
            _truck.Driver = newDriver;
            if (_truck.Driver == newDriver)
            {
                Console.WriteLine("Водитель {0} был заменён на {1}", oldDriver, newDriver);
            }
        }

        public void StartRun()
        {
            _truck.ChangeState(new InRunState(_truck));
            _truck.State = InRunState.Name;
            Console.WriteLine($"ГРузовик {_truck.Name} отправился в путь!");
        }

        public void StartRepair()
        {
            _truck.ChangeState(new InRepairState(_truck));
            _truck.State = InRepairState.Name;
            Console.WriteLine($"Грузовик {_truck.Name}  ремонтируется!");
        }

        public string ToString()
        {
            return Name;
        }
    }
}