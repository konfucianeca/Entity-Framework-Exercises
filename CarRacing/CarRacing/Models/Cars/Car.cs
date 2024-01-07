using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using System;

namespace CarRacing.Models.Cars
{
    public abstract class Car : ICar
    {
        private string make;
        private string model;
        private string vin;
        private int horsepower;
        private double fuelAvailable;
        private double fuelConsumtpionPerRace;

        protected Car(string make, string model, string VIN, int horsePower, double fuelAvailable, double fuelConsumptionPerRace)
        {
            this.Make = make;
            this.Model = model;
            this.VIN = VIN;
            this.HorsePower = horsePower;
            this.FuelAvailable = fuelAvailable;
            this.FuelConsumptionPerRace = fuelConsumptionPerRace;
        }
        public string Make
        {
            get => this.make;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidCarMake);
                }
                this.make = value;
            }
        }

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidCarModel);
                }
                this.model = value;
            }
        }

        public string VIN
        {
            get => this.vin;
            private set
            {
                if (value.Length!=17)
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidCarVIN);
                }
                this.vin = value;
            }
        }

        public int HorsePower
        {
            get => this.horsepower;
            protected set
            {
                if (value<0)
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidCarHorsePower);
                }
                this.horsepower = value;
            }
        }

        public double FuelAvailable
        {
            get => this.fuelAvailable;
            private set
            {
                if (value < 0)
                {
                    this.fuelAvailable = 0;
                }
                this.fuelAvailable = value;
            }
        }

        public double FuelConsumptionPerRace
        {
            get => this.fuelConsumtpionPerRace;
            private set
            {
                if (value<0)
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidCarFuelConsumption);
                }
                this.fuelConsumtpionPerRace = value;
            }
        }

        public virtual void Drive()
        {
            this.FuelAvailable -= this.FuelConsumptionPerRace;
        }
    }
}
