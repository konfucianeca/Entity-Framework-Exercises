using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private IRepository<ICar> cars;
        private IRepository<IDriver> drivers;
        private IRepository<IRace> races;
        public ChampionshipController()
        {
            this.cars = new CarRepository();
            this.drivers = new DriverRepository();
            this.races = new RaceRepository();
        }

        public string CreateDriver(string driverName)
        {
            if (this.drivers.GetByName(driverName) != null)
            {
                throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.DriversExists, driverName));
            }

            Driver newDriver = new Driver(driverName);
            drivers.Add(newDriver);
            return string.Format(Utilities.Messages.OutputMessages.DriverCreated, driverName);
        }
        public string CreateCar(string type, string model, int horsePower)
        {
            if (this.cars.GetByName(model) != null)
            {
                throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.CarExists, model));
            }

            ICar newCar = null;
            string carType = "";
            if (type == "Muscle")
            {
                newCar = new MuscleCar(model, horsePower);
                carType = "MuscleCar";
            }
            else if (type == "Sports")
            {
                newCar = new SportsCar(model, horsePower);
                carType = "SportsCar";
            }

            this.cars.Add(newCar);
            return string.Format(Utilities.Messages.OutputMessages.CarCreated, carType, model);
        }
        public string AddCarToDriver(string driverName, string carModel)
        {
            if (this.drivers.GetByName(driverName) == null)
            {
                throw new InvalidOperationException(string.Format(Utilities.Messages.ExceptionMessages.DriverNotFound, driverName));
            }
            if (this.cars.GetByName(carModel) == null)
            {
                throw new InvalidOperationException(string.Format(Utilities.Messages.ExceptionMessages.CarNotFound, carModel));
            }
            var carToDriver = this.cars.GetByName(carModel);
            drivers.GetByName(driverName).AddCar(carToDriver);
            return string.Format(Utilities.Messages.OutputMessages.CarAdded, driverName, carModel);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            if (this.races.GetByName(raceName) == null)
            {
                throw new InvalidOperationException(string.Format(Utilities.Messages.ExceptionMessages.RaceNotFound, raceName));
            }
            if (this.drivers.GetByName(driverName) == null)
            {
                throw new InvalidOperationException(string.Format(Utilities.Messages.ExceptionMessages.DriverNotFound, driverName));
            }

            Driver driverToRace = (Driver)drivers.GetByName(driverName);
            races.GetByName(raceName).AddDriver(driverToRace);
            return string.Format(Utilities.Messages.OutputMessages.DriverAdded, driverName, raceName);
        }
        public string CreateRace(string name, int laps)
        {
            if (races.GetByName(name) != null)
            {
                throw new InvalidOperationException(string.Format(Utilities.Messages.ExceptionMessages.RaceExists, name));
            }

            Race raceToAdd = new Race(name, laps);
            races.Add(raceToAdd);
            return string.Format(Utilities.Messages.OutputMessages.RaceCreated, name);
        }

        public string StartRace(string raceName)
        {
            int minimumDrivers = 3;

            if (races.GetByName(raceName) == null)
            {
                throw new InvalidOperationException(string.Format(Utilities.Messages.ExceptionMessages.RaceNotFound, raceName));
            }
            if (races.GetByName(raceName).Drivers.Count < 3)
            {
                throw new InvalidOperationException(string.Format(Utilities.Messages.ExceptionMessages.RaceInvalid, raceName, minimumDrivers));
            }

            IRace currRace = races.GetByName(raceName);
            List<IDriver> raceDrivers = races.GetByName(raceName).Drivers.ToList();
            Dictionary<string, double> driversRacePoints = new Dictionary<string, double>();
            foreach (IDriver raceDriver in raceDrivers)
            {
                double points = raceDriver.Car.CalculateRacePoints(currRace.Laps);
                driversRacePoints.Add(raceDriver.Name, points);
            }
            //dict = dict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            driversRacePoints = driversRacePoints.OrderByDescending(d => d.Value).ToDictionary(d => d.Key, d => d.Value);
            List<string> driversNames = new List<string>();
            foreach (var raceDriver in driversRacePoints)
            {
                driversNames.Add(raceDriver.Key);
            }
            //raceDrivers.OrderByDescending(d => d.Car.CalculateRacePoints(currRace.Laps));

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Driver {driversNames[0]} wins {raceName} race.");
            sb.AppendLine($"Driver {driversNames[1]} is second in {raceName} race.");
            sb.AppendLine($"Driver {driversNames[2]} is third in {raceName} race.");
            return sb.ToString().Trim();
        }
    }
}
