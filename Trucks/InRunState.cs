using System;

namespace Trucks
{
    public class InRunState : IState
    {
        private Truck _truck;
        public static string Name = "run";
        
        public InRunState(Truck truck) => _truck = truck;
        
        public void ChangeDriver()
        {
            Console.WriteLine("Так как водитель в пути - заменить его нельзя!");
        }

        public void StartRun()
        {
            Console.WriteLine("Грузовик и так в пути!");
        }

        public void StartRepair()
        {
            _truck.ChangeState(new InRepairState(_truck));
            _truck.State = InRepairState.Name;
            Console.WriteLine($"Грузовик {_truck.Name} ремонтируется!");
        }
    }
}