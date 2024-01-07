namespace CarRacing.Models.Cars
{
    public class TunedCar : Car
    {
        private const double AvailableFuel = 65.0;
        private const double FuelConsPerRace = 7.5;
        public TunedCar(string make, string model, string VIN, int horsePower) : base(make, model, VIN, horsePower, AvailableFuel, FuelConsPerRace)
        {
        }

        public override void Drive()
        {
            base.Drive();
            this.HorsePower = (int)System.Math.Round(this.HorsePower * 0.97);
        }
    }
}
