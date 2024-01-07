using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using CarRacing.Repositories.Contracts;
using System;
using System.Linq;
using System.Text;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        private IRepository<ICar> cars;
        private IRepository<IRacer> racers;
        private IMap map;

        public Controller()
        {
            this.cars = new CarRepository();
            this.racers = new RacerRepository();
            this.map = new Map();
        }

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            ICar car = null;
            if (type == "SuperCar")
            {
                car = new SuperCar(make, model, VIN, horsePower);
            }
            else if (type == "TunedCar")
            {
                car = new TunedCar(make, model, VIN, horsePower);
            }
            else
            {
                throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidCarType);
            }

            cars.Add(car);
            return string.Format(Utilities.Messages.OutputMessages.SuccessfullyAddedCar, make, model, VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            IRacer racer = null;
            ICar car = cars.FindBy(carVIN);

            if (car == null)
            {
                throw new ArgumentException(Utilities.Messages.ExceptionMessages.CarCannotBeFound);
            }
            if (type == "ProfessionalRacer")
            {
                racer = new ProfessionalRacer(username, car);
            }
            else if (type == "StreetRacer")
            {
                racer = new StreetRacer(username, car);
            }
            else
            {
                throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidRacerType);
            }

            racers.Add(racer);
            return string.Format(Utilities.Messages.OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IRacer racerOne = racers.FindBy(racerOneUsername);
            if (racerOne == null)
            {
                throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.RacerCannotBeFound, racerOneUsername));
            }

            IRacer racerTwo = racers.FindBy(racerTwoUsername);
            if (racerTwo == null)
            {
                throw new ArgumentException(string.Format(Utilities.Messages.ExceptionMessages.RacerCannotBeFound, racerTwoUsername));
            }

            return map.StartRace(racerOne, racerTwo);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var racer in racers.Models.OrderByDescending(r => r.DrivingExperience).ThenBy(r => r.Username)) 
            {
                sb.AppendLine(racer.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
