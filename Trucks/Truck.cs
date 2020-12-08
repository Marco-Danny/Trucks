namespace Trucks
{
    public class Truck
    {
        public int Id { get; }
        public string Name { get; }
        public string Driver { get; }
        public string State { get; }

        public Truck(int id, string name, string driver, string state)
        {
            Id = id;
            Name = name;
            Driver = driver;
            State = state;
        }

        public override string ToString()
        {
            var truckInformation =
                $"№	\t| {Id.ToString()}\n" +
                $"Марка	\t| {Name}\n" +
                $"Водитель\t| {Driver}\n" +
                $"Состояние\t| {State}\n";
            return truckInformation;
        }
    }
}