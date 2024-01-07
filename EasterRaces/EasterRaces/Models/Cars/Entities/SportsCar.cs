using System;

namespace EasterRaces.Models.Cars.Entities
{
    public class SportsCar : Car
    {
        private const double SCCubicCentimeters = 3000;
        private const int MinHorsePower = 250;
        private const int MaxHorsePower = 450;
        private int horsePower;
        public SportsCar(string model, int horsePower)
            : base(model, horsePower, SCCubicCentimeters, MinHorsePower, MaxHorsePower)
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

        public override double CubicCentimeters => SCCubicCentimeters;
    }
}
