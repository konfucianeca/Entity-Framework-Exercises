namespace CarRacing.Models.Cars
{
    public class SuperCar : Car
    {
        private const double AvailableFuel = 80.0;
        private const double FuelConsPerRace = 10.0;

        public SuperCar(string make, string model, string VIN, int horsePower) : base(make, model, VIN, horsePower, AvailableFuel, FuelConsPerRace)
        {
        }
    }
}
