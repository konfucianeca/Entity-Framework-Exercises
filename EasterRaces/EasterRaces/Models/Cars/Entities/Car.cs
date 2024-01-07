using EasterRaces.Models.Cars.Contracts;
using System;

namespace EasterRaces.Models.Cars.Entities
{
    public abstract class Car : ICar
    {
        private string model;
        private const int CarModelNameNumberOfSymbols = 4;
        protected Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            this.Model = model;
            this.HorsePower = horsePower;
        }

        public string Model
        {
            get { return this.model; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < CarModelNameNumberOfSymbols)
                {
                    throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.InvalidModel, value, CarModelNameNumberOfSymbols));
                }
                model = value;
            }
        }

        public abstract int HorsePower { get; set; }
        public abstract double CubicCentimeters { get; }

        public double CalculateRacePoints(int laps)
        {
            return this.CubicCentimeters / this.HorsePower * laps;
        }
    }
}
