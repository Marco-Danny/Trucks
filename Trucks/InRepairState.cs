using System;
using System.Collections.Generic;

namespace Trucks
{
    public class InRepairState : IState
    {
        private Truck _truck;
        public static string Name = "repair";

        public InRepairState(Truck truck) => _truck = truck;

        public void ChangeDriver()
        {
            Console.WriteLine("Так как водитель в пути - заменить его нельзя!");
        }

        public void StartRun()
        {
            string truckGo = $"Грузовик {_truck.Name} отпраился в путь!";
            var truckRepair = $"Грузовик {_truck.Name} поехал на базу!";
            var InRun = new InRunState(_truck);
            var inBase = new InBaseState(_truck);
            var states = new List<IState>();
            states.Add(inBase);
            states.Add(InRun);
            Random random = new Random();
            int stateIndex = random.Next(2);
            IState changeState = states[stateIndex];
            _truck.ChangeState(changeState);
            Console.WriteLine(changeState == InRun ? truckGo : truckRepair);
        }

        public void StartRepair()
        {
            Console.WriteLine("Грузовик и так ремонтируется!");
        }
    }
}