using System;

namespace EasterRaces.Models.Cars.Entities
{
    public class MuscleCar : Car
    {
        private const double MKCubicCentimeters = 5000;
        private const int MinHorsePower = 400;
        private const int MaxHorsePower = 600;
        private int horsePower;
        public MuscleCar(string model, int horsePower)
            : base(model, horsePower, MKCubicCentimeters, MinHorsePower, MaxHorsePower)
        {
        }

        public override int HorsePower
        {
            get => this.horsePower;
            set
            {
                if (value < MinHorsePower || value > MaxHorsePower)
                {
                    throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.InvalidHorsePower, value));
                }
                this.horsePower = value;
            }
        }
        public override double CubicCentimeters => MKCubicCentimeters;
    }
}
