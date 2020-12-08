using System.Runtime.Serialization;

namespace Trucks
{
    [DataContract]
    public class Truck : IState
    {
        [DataMember] public int Id { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public string Driver { get; set; }
        [DataMember] public string State { get; set; }
        public IState _status { get; private set; }

        public Truck(int id, string name, string driver, string state)
        {
            Id = id;
            Name = name;
            Driver = driver;
            State = state;
            SetStatus();
        }

        private void SetStatus()
        {
            switch (State)
            {
                case "base":
                    _status = new InBaseState(this);
                    break;
            }
        }

        public void ChangeState(IState other_status)
        {
            _status = other_status;
            State = other_status.ToString();
        }

        public void ChangeDriver() => _status.ChangeDriver();

        public void StartRun() => _status.StartRun();

        public void StartRepair() => _status.StartRepair();

        public override string ToString()
        {
            var truckInformation =
                $"№	\t| {Id.ToString()}\n" +
                $"Марка	\t| {Name}\n" +
                $"Водитель\t| {Driver}\n" +
                $"Состояние\t| {State.ToString()}\n";
            return truckInformation;
        }
    }
}