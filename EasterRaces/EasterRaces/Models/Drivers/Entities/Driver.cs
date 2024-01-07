using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using System;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private const int NameLenght = 5;
        private string name;
        public Driver(string name)
        {
            this.Name = name;
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < NameLenght)
                {
                    throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.InvalidName, value, NameLenght));
                }
                this.name = value;
            }
        }

        public ICar Car { get; private set; }

        public int NumberOfWins { get; set; }

        public bool CanParticipate
        {
            get
            {
                if (this.Car != null)
                {
                    return true;
                }
                return false;
            }
        }

        public void AddCar(ICar car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(Utilities.Messages.ExceptionMessages.CarInvalid);
            }
            this.Car = car;
        }

        public void WinRace()
        {
            this.NumberOfWins++;
        }
    }
}
