using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EasterRaces.Models.Races.Entities
{
    public class Race : IRace
    {
        private const int NameLenght = 5;
        private string name;
        private int laps;
        private const int WrongNumberOfLaps = 1;
        private readonly IList<IDriver> drivers;
        public Race(string name, int laps)
        {
            this.Name = name;
            this.Laps = laps;
            this.drivers = new List<IDriver>();
        }
        public string Name
        {
            get { return name; }
            private set 
            {
                if (string.IsNullOrEmpty(value) || value.Length < NameLenght)
                {
                    throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.InvalidName, value, NameLenght));
                }
                name = value;
            }
        }

        public int Laps
        {
            get=>this.laps;
            private set
            {
                if (value<1)
                {
                    throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.InvalidNumberOfLaps, WrongNumberOfLaps));
                }
                this.laps= value;
            }
        }

        public IReadOnlyCollection<IDriver> Drivers => (IReadOnlyCollection<IDriver>)this.drivers;

        public void AddDriver(IDriver driver)
        {
            if (driver==null)
            {
                throw new ArgumentNullException(Utilities.Messages.ExceptionMessages.DriverInvalid);
            }
            else if (!driver.CanParticipate)
            {
                throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.DriverNotParticipate, driver.Name));
            }
            else if (this.drivers.Contains(driver))
            {
                throw new ArgumentNullException(string.Format(Utilities.Messages.ExceptionMessages.DriverAlreadyAdded, driver.Name, this.Name));
            }
            else
            {
                this.drivers.Add(driver);
            }
        }
    }
}
